using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AvantRestAPI.Models;
using Microsoft.Extensions.Configuration;

namespace AvantRestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContractorController : Controller
    {
        private readonly IDbRepository _db;

        public ContractorController(IDbRepository repository)
        {
            _db = repository;
        }

        [HttpGet]
        [Route("Index")]
        public JsonResult Index()
        {
            return new JsonResult(_db.Contractors);
        }

        [HttpPost]
        [Route("Create")]
        public JsonResult CreateContractor(Contractor contractor)
        {
            try
            {
                _db.AddContractor(contractor);
            }
            catch (Exception e)
            {
                return new JsonResult($"Error : {e.Message}");
            }
        }
    }
}
