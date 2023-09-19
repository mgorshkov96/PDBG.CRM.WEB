using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models;
using PDBG.CRM.WEB.Models.Repositories;
using Newtonsoft.Json.Linq;

namespace PDBG.CRM.WEB.Controllers
{
    [Route("api/location")]
    public class LocationApiController : Controller
    {        
        private ILocationLogRepository _locationLogRepository;
        private IAgentStateRepository _agentStateRepository;

		public LocationApiController(ILocationLogRepository locationLogRepository, IAgentStateRepository agentStateRepository)
        {
            _locationLogRepository = locationLogRepository;
            _agentStateRepository = agentStateRepository;
        }

        [HttpGet]
        public async Task<IActionResult> AgentLocations()
        {           
            var locations = await _agentStateRepository.AgentStates.ToListAsync();
            return Ok(locations);
        }

        [HttpPost]
        public async Task<IActionResult> AddLog([FromBody]LocationLog locationLog)
        {
            if (locationLog == null)
            {
                return BadRequest();
            }           

            await _locationLogRepository.AddLocationLogAsync(locationLog);
            
            return Ok(locationLog);
        }
    }
}
