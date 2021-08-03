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
    public class HomeController : Controller
    {
        private readonly IDbRepository _db;

        public HomeController(IDbRepository repository)
        {
            _db = repository;
        }

        [HttpGet]
        [Route("Index")]
        public JsonResult Index()
        {
            return new(_db.Contractors);
        }

        [HttpPost]
        [Route("Create")]
        public JsonResult Create([FromBody] Contractor contractor)
        {
            try
            {
                return new(_db.AddContractor(contractor));
            }
            catch (Exception e)
            {
                return new($"Error : {e.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public JsonResult Update([FromBody] Contractor contractor)
        {
            try
            {
                return new(_db.UpdateContractor(contractor));
            }
            catch (Exception e)
            {
                return new($"Error : {e.Message}");
            }
        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _db.RemoveContractor(id);
                return Ok();
            }
            catch (Exception e)
            {
                return new JsonResult($"Error : {e.Message}");
            }
        }
    }
}
