using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Dadata;
using Dadata.Model;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace AvantRestAPI.Models
{
    public class LiteDbRepository : IDbRepository, IDisposable
    {
        private readonly LiteDatabase _database;
        private const string DaDataApiToken = "921e88969c6d8fa637fe7f5c3805e04c8ada1dff";
        private readonly SuggestClientAsync _api = new SuggestClientAsync(DaDataApiToken);

        public IEnumerable<Contractor> Contractors => _database.GetCollection<Contractor>("Contractors").FindAll();

        public LiteDbRepository(IConfiguration configuration)
        {
            _database = new LiteDatabase(@configuration["ConnectionStrings:LiteDbConnection"]);
        }

        public Contractor AddContractor(Contractor contractor)
        {
            //Валидация полей.
            contractor.Name = contractor.Name.Trim();
            contractor.Kpp = contractor.Kpp.Trim();
            contractor.Inn = contractor.Inn.Trim();
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(contractor, new ValidationContext(contractor), results, true))
                throw new ArgumentException(results[0].ErrorMessage);

            var contractorFromDaData = GetContractorFromDaData(contractor.Type, contractor.Inn, contractor.Kpp).Result;
            if (contractorFromDaData != null)
            {
                    Contractor newContractor = new Contractor()
                    {
                        Name = contractor.Name,
                        FullName = contractorFromDaData.name.full_with_opf,
                        Inn = contractor.Inn,
                        Kpp = contractor.Kpp,
                        Type = contractor.Type
                    };
                    _database.GetCollection<Contractor>("Contractors").Insert(newContractor);
                    return newContractor;
            }

            throw new ArgumentException($"Contractor with inn = {contractor.Inn} and kpp = {contractor.Kpp} not found on DaData");
        }

        public Contractor UpdateContractor(Contractor contractor)
        {
            //Валидация полей.
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateObject(contractor, new ValidationContext(contractor), results, true))
                throw new ArgumentException(results[0].ErrorMessage);

            Contractor contractorToUpdate = Contractors.FirstOrDefault(c=> c.Id == contractor.Id);
            if (contractorToUpdate != null)
            {
                contractorToUpdate.Type = contractor.Type;
                contractorToUpdate.Name = contractor.Name;
                if (contractorToUpdate.Inn != contractor.Inn || contractorToUpdate.Kpp != contractor.Kpp)
                {
                    var contractorFromDaData = GetContractorFromDaData(contractor.Type, contractor.Inn, contractor.Kpp).Result;
                    if (contractorFromDaData != null)
                    {
                        contractorToUpdate.Inn = contractor.Inn;
                        contractorToUpdate.Kpp = contractor.Kpp;
                    }
                    else
                    {
                        throw new ArgumentException(
                            $"New INN {contractor.Inn} and KPP {contractor.Kpp} are incorrect. Contractor with this params wasn't " +
                            $"founded on DaData");
                    }
                }
                contractorToUpdate.FullName = contractor.FullName;
                _database.GetCollection<Contractor>("Contractors").Update(contractorToUpdate);
            }
            throw new ArgumentException($"There is no contractor with id = {contractor.Id}");
        }

        public void RemoveContractor(Contractor contractor)
        {
            Contractor contractorToDelete = Contractors.FirstOrDefault(c => c.Inn == contractor.Inn
                                   && c.Kpp == contractor.Kpp
                                   && c.Name == contractor.Name
                                   && c.Type == contractor.Type
                                   && c.FullName == contractor.FullName
                                   && c.Id == contractor.Id);
            if (contractorToDelete != null)
                _database.GetCollection<Contractor>("Contractors").Delete(contractor.Id);
            else
                throw new ArgumentException($"There is no contractor with id = {contractor.Id}");
        }

        public void RemoveContractor(int id)
        {
            Contractor contractorToDelete = Contractors.FirstOrDefault(c => c.Id == id);
            if (contractorToDelete != null)
                _database.GetCollection<Contractor>("Contractors").Delete(id);
            else
                throw new ArgumentException($"There is no contractor with id = {id}");
        }

        private async Task<Party> GetContractorFromDaData(ContractorType type, string inn, string kpp = "")
        {
            var request = type == ContractorType.Individual ? new FindPartyRequest(query: inn) : new FindPartyRequest(query: inn, kpp: kpp);
            var response = await _api.FindParty(request);
            return response.suggestions.Count > 0 ? response.suggestions[0].data : null;
        }

        public void Dispose()
        {
            _database?.Dispose();
        }
    }
}
