using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace api.Controllers
{
    [Route("api/records")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        // GET: api/records
        [HttpGet]
        public IEnumerable<BusinessContactRecord> Get()
        {
            return new BusinessContactRecord[]{ };
        }

        // GET api/records/5
        [HttpGet("{id}")]
        public BusinessContactRecord Get(int id)
        {
            return new BusinessContactRecord();
        }

        // POST api/records
        [HttpPost]
        public void Post([FromBody] BusinessContactRecord value)
        {

        }

        // PUT api/records/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] BusinessContactRecord value)
        {

        }

        // DELETE api/records/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {

        }
    }
}
