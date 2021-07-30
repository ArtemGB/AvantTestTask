using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dadata;
using Dadata.Model;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace AvantRestAPI.Models
{
    public class LiteDbRepository : IDbRepository
    {
        private readonly LiteDatabase _database;
        private const string DaDataApiToken = "921e88969c6d8fa637fe7f5c3805e04c8ada1dff";
        private readonly SuggestClientAsync _api = new SuggestClientAsync(DaDataApiToken);

        public IEnumerable<Contractor> Contractors => _database.GetCollection<Contractor>("Contractors").FindAll();

        public LiteDbRepository(IConfiguration configuration)
        {
            _database = new LiteDatabase(@configuration["ConnectionStrings:LiteDbConnection"]);
        }

        ~LiteDbRepository()
        {
            _database.Dispose();
        }
        public void AddContractor(Contractor contractor)
        {
            var contractorFromDaData = GetContractorFromDaData(contractor.Type, contractor.INN, contractor.KPP).Result;
            if (contractorFromDaData != null)
                _database.GetCollection<Contractor>("Contractors").Insert(new Contractor()
                {
                    Name = contractor.Name,
                    FullName = contractorFromDaData.name.full_with_opf,
                    INN = contractor.INN,
                    KPP = contractor.KPP,
                    Type = contractor.Type
                });
            throw new ArgumentException($"Contractor with inn = {contractor.INN} and kpp = {contractor.KPP} not found on DaData");
        }

        public void RemoveContractor(Contractor contractor)
        {
            Contractor contractorToDelete = Contractors.FirstOrDefault(c => c.INN == contractor.INN
                                   && c.KPP == contractor.KPP
                                   && c.Name == contractor.Name
                                   && c.Type == contractor.Type
                                   && c.FullName == contractor.FullName
                                   && c.Id == contractor.Id);
            if (contractorToDelete != null)
            {
                _database.GetCollection<Contractor>("Contractors").Delete(contractor.Id);
            }
        }

        public void RemoveContractor(int id)
        {
            Contractor contractorToDelete = Contractors.FirstOrDefault(c => c.Id == id);
            if (contractorToDelete != null)
                _database.GetCollection<Contractor>("Contractors").Delete(id);
        }

        private async Task<Party> GetContractorFromDaData(ContractorType type, string inn, string kpp = "")
        {
            var request = type == ContractorType.Individual ? new FindPartyRequest(query: inn) : new FindPartyRequest(query: inn, kpp: kpp);
            var response = await _api.FindParty(request);
            return response.suggestions.Count > 0 ? response.suggestions[0].data : null;
        }
    }
}
