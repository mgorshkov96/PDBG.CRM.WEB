using Microsoft.AspNetCore.Mvc;
using PDBG.CRM.WEB.Models;
using System.Text.Json;

namespace PDBG.CRM.WEB.Controllers
{
    [ApiController]
    [Route("api/leads")]
    public class LeadsApiController : ControllerBase
    {
        private Models.AppContext db;

        public LeadsApiController(Models.AppContext db)
        {
            this.db = db;
        }


        [HttpPost]
        public IActionResult Post(JsonDocument document)
        {
            
            return Ok();
        }
    }
}
