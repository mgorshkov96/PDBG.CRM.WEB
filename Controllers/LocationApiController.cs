using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models;
using Newtonsoft.Json.Linq;

namespace PDBG.CRM.WEB.Controllers
{
    [Route("api/location")]
    public class LocationApiController : Controller
    {
        MyContext db;
        public LocationApiController(MyContext context)
        {
            this.db = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AgentState>>> AgentLocations()
        {
            //var locations = await Task.Run(() => db.GetAgentLocations());
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

            //LocationLog locationLog = new LocationLog();
            //locationLog.LocDate = jLocationLog["locDate"].ToObject<DateTime>();
            //locationLog.EmployeeId = jLocationLog["employeeId"].ToObject<int>();
            //locationLog.Lat = jLocationLog["lat"].ToObject<decimal>();
            //locationLog.Lng = jLocationLog["lng"].ToObject<decimal>();

            //var employee = db.Employees.FirstOrDefaultAsync(x => x.Id == locationLog.EmployeeId);

            //if (employee.Result == null)
            //{
            //    return NotFound();
            //}

            //locationLog.Employee = employee.Result;

            db.LocationLogs.Add(locationLog);
            await db.SaveChangesAsync();
            return Ok(locationLog);
        }
    }
}
