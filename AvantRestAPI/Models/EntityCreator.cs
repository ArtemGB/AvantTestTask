using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dadata;
using Dadata.Model;

namespace AvantRestAPI.Models
{
    public class EntityCreator
    {
        private const string DaDataApiToken = "921e88969c6d8fa637fe7f5c3805e04c8ada1dff";
        private const string DaDataKey = "4468db5d2d1c6b5628fae74058f002371ea2766a";
        private SuggestClientAsync _api = new SuggestClientAsync(DaDataApiToken, DaDataKey);

        public Contractor CreateContractor(string name, ContractorType type, string inn, string kpp = "")
        {
            var contractor = GetContractorFromDaData(type, inn, kpp).Result;
            if (contractor != null)
                return new Contractor()
                {
                    Name = name,
                    FullName = contractor.name.full_with_opf,
                    INN = inn,
                    KPP = kpp,
                    Type = type
                };
            throw new ArgumentException($"Contractor with inn = {inn} and kpp = {kpp} not found on DaData");
        }

        protected async Task<Party> GetContractorFromDaData(ContractorType type, string inn, string kpp = "")
        {
            var request = type == ContractorType.Individual ? new FindPartyRequest(query: inn) : new FindPartyRequest(query: inn, kpp: kpp);
            var response = await _api.FindParty(request);
            return response.suggestions.Count > 0 ? response.suggestions[0].data : null;
        }
    }
}
