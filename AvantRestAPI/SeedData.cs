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
            EntityCreator creator = new EntityCreator();
            db.AddContractor(creator.CreateContractor("Sber", ContractorType.Legal, "7707083893", "773643001"));
            db.AddContractor(creator.CreateContractor("МКБ", ContractorType.Legal, "7734202860", "770801001"));
            db.AddContractor(creator.CreateContractor("Cofix", ContractorType.Legal, "7728339641", "770601001"));
            db.AddContractor(creator.CreateContractor("Sber", ContractorType.Legal, "7707083893", "773643001"));
            db.AddContractor(creator.CreateContractor("Sber", ContractorType.Legal, "7707083893", "773643001"));
        }
    }
}
