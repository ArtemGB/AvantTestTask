using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AvantRestAPI.Models
{
    public class EntityCreator
    {
        private const string DaDataApiToken = "921e88969c6d8fa637fe7f5c3805e04c8ada1dff";
        private const string DaDataKey = "4468db5d2d1c6b5628fae74058f002371ea2766a";

        public Contractor CreateContractor(string name, ContractorType type, string inn, string kpp = "")
        {
            if (CheckContractorOnDaData(type, name, kpp))
                return new Contractor()
                {
                    Name = name,
                    FullName = "",
                    INN = inn,
                    KPP = kpp,
                    Type = type
                };
            throw new ArgumentException($"Contractor with inn = {inn} and kpp = {kpp} not found on DaData");
        }

        protected bool CheckContractorOnDaData(ContractorType type, string inn, string kpp = "")
        {
            return true;
        }
    }
}
