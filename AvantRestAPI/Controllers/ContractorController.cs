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
        private readonly IDbRepository db;

        public ContractorController(IDbRepository repository)
        {
            db = repository;
        }

        [HttpGet]
        [Route("Index")]
        public JsonResult Index()
        {
            return new JsonResult(db.Contractors);
        }
    }
}
