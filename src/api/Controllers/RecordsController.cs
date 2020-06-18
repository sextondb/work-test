using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models;
using api.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/users/{userId}/records")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly BusinessContactRecordRepository recordRepository;

        public RecordsController(BusinessContactRecordRepository recordRepository)
        {
            this.recordRepository = recordRepository;
        }

        // GET: api/users/1/records
        [HttpGet]
        public async Task<IEnumerable<BusinessContactRecord>> Get(int userId)
        {
            return await recordRepository.GetAllAsync(userId);
        }

        // GET: api/users/1/records/paged?page=1&pageSize=5
        [HttpGet("paged")]
        public async Task<PagedResult<IEnumerable<BusinessContactRecord>>> GetPaged(int userId, int page, int? pageSize)
        {
            if (!pageSize.HasValue)
            {
                pageSize = 5;
            }

            return await recordRepository.GetAllPagedAsync(userId, pageSize.Value, page);
        }

        // GET api/users/1/records/5
        [HttpGet("{id}", Name = nameof(GetById))]
        public async Task<ActionResult<BusinessContactRecord>> GetById(int userId, int id)
        {
            var record = await recordRepository.GetAsync(userId, id);
            if (record == null)
            {
                return NotFound();
            }
            return Ok(record);
        }

        // POST api/users/1/records
        [HttpPost]
        public async Task<ActionResult> Post(int userId, [FromBody] BusinessContactRecord record)
        {
            var id = await recordRepository.InsertAsync(userId, record);
            record.Id = id;
            record.UserId = userId;
            return CreatedAtRoute(nameof(GetById), new { userId, id }, record);
        }

        // PUT api/users/1/records/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int userId, int id, [FromBody] BusinessContactRecord record)
        {
            var rowsUpdatd = await recordRepository.UpdateAsync(userId, id, record);
            if (rowsUpdatd == 0)
            {
                // Cannot use PUT to create rows, use POST instead
                return BadRequest();
            }
            else
            {
                return NoContent();
            }
        }

        // DELETE api/users/1/records/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int userId, int id)
        {
            await recordRepository.DeleteAsync(userId, id);
            return NoContent();
        }
    }
}
