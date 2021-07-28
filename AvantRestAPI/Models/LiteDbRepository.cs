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
        private LiteDatabase _database;
        public IEnumerable<Contractor> Contractors => _database.GetCollection<Contractor>("Contractors").FindAll();

        public LiteDbRepository(IConfiguration configuration)
        {
            _database = new LiteDatabase(@configuration["ConnectionStrings:LiteDbConnection"]);
        }
    }
}
