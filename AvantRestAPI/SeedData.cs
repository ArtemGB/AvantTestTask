using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvantRestAPI.Models;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace AvantRestAPI
{
    public class SeedData
    {
        public static void SeedContractors(IConfiguration configuration)
        {
            LiteDbRepository db = new LiteDbRepository(configuration);
            var contractors = db.Contractors;
            if (!db.Contractors.Any())
            {
                db.AddContractor(new Contractor(){ Name = "Sber", INN = "7707083893", KPP = "773643001", Type = ContractorType.Legal});
                db.AddContractor(new Contractor(){ Name = "МКБ", INN = "7734202860", KPP = "770801001", Type = ContractorType.Legal});
                db.AddContractor(new Contractor(){ Name = "Cofix", INN = "7728339641", KPP = "770601001", Type = ContractorType.Legal});
            }
        }
    }
}
