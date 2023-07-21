using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models;
using System.Data.Common;

namespace PDBG.CRM.WEB.Controllers
{
    [Route("api/clients")]
    [ApiController]
    public class ClientsApiController : ControllerBase
    {
        MyContext db;
        public ClientsApiController(MyContext context)
        {
            this.db = context;
        }

        //[HttpGet]
        //public IEnumerable<Client> Get() => db.Clients.ToList();

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> Get()
        {
            return await db.Clients.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> Get(int id)
        {
            var client = await db.Clients.FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
            {
                return NotFound();
            }
            return new ObjectResult(client);
        }

        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var client = db.Clients.SingleOrDefault(c => c.Id == id);

        //    if (client == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(client);
        //}

        [HttpPost]
        public async Task<ActionResult<Client>> Post(Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }

            db.Clients.Add(client);
            await db.SaveChangesAsync();
            return Ok(client);
        }

        [HttpPost("AddClient")]
        public async Task<ActionResult<Client>> PostBody([FromBody] Client client) =>
            await Post(client);

        [HttpPut]
        public async Task<ActionResult<Client>> Put(Client client)
        {
            if (client == null)
            {
                return BadRequest();
            }
            if (!db.Clients.Any(x => x.Id == client.Id))
            {
                return NotFound();
            }

            db.Update(client);
            await db.SaveChangesAsync();
            return Ok(client);
        }

        [HttpPut("UpdateClient")]
        public async Task<ActionResult<Client>> PutBody([FromBody] Client client) =>
            await Put(client);

        [HttpDelete("{id}")]
        public async Task <ActionResult<Client>> Delete(int id)
        {
            var client = db.Clients.FirstOrDefault(c => c.Id == id);

            if (client == null)
            {
                return NotFound();
            }
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return Ok(client);
        }
    }
}
