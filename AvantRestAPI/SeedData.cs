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
            using var db = new LiteDbRepository(configuration);
            var contractors = db.Contractors;
            if (!db.Contractors.Any())
            {
                db.AddContractor(new Contractor(){ Name = "Sber", Inn = "7707083893", Kpp = "773643001", Type = ContractorType.Legal});
                db.AddContractor(new Contractor(){ Name = "МКБ", Inn = "7734202860", Kpp = "770801001", Type = ContractorType.Legal});
                db.AddContractor(new Contractor(){ Name = "Cofix", Inn = "7728339641", Kpp = "770601001", Type = ContractorType.Legal});
            }
        }
    }
}
