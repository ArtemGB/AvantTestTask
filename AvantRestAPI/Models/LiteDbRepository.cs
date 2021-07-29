using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Microsoft.Extensions.Configuration;

namespace AvantRestAPI.Models
{
    public class LiteDbRepository : IDbRepository
    {
        private readonly LiteDatabase _database;
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
            _database.GetCollection<Contractor>("Contractors").Insert(contractor);
        }

        public void RemoveContractor(Contractor contractor)
        {
            _database.GetCollection<Contractor>("Contractors").Delete(contractor.Id);
        }

        public void RemoveContractor(int Id)
        {
            _database.GetCollection<Contractor>("Contractors").Delete(Id);
        }
    }
}
