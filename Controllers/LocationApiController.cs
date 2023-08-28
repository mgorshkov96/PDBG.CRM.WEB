using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models;
using Newtonsoft.Json.Linq;

namespace PDBG.CRM.WEB.Controllers
{
    [Route("api/location")]
    public class LocationApiController : Controller
    {
        Models.AppContext db;
        public LocationApiController(Models.AppContext context)
        {
            this.db = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AgentState>>> AgentLocations()
        {           
            var locations = await db.AgentStates.ToListAsync();
            return (locations);
        }

        [HttpPost]
        public async Task<ActionResult<LocationLog>> AddLog([FromBody]LocationLog locationLog)
        {
            if (locationLog == null)
            {
                return BadRequest();
            }           

            db.LocationLogs.Add(locationLog);
            await db.SaveChangesAsync();
            return Ok(locationLog);
        }
    }
}
