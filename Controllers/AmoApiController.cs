using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models;
using PDBG.CRM.WEB.Models.AmoEntities;

namespace PDBG.CRM.WEB.Controllers
{
    [Route("amo/webhook")]
    [ApiController]
    public class AmoApiController : ControllerBase
    {
        private Models.AppContext db;

        public AmoApiController(Models.AppContext context)
        {
            this.db = context;
        }

        [HttpPost]
        public async Task<IActionResult> CatchWebhook([FromForm] AmoWebhook webhook)
        {

            if (webhook == null || webhook.Leads == null)
            {
                return BadRequest();
            }

            AmoWebhookLead webhookLead = webhook.Leads.status[0];

            var lead = db.Leads.FirstOrDefaultAsync(x => x.Id == webhookLead.id);

            if (lead != null)
            {
                return Ok();
            }

            //Lead lead = new Lead();
            //lead.Id = webhookLead.id;
            //lead.Created = DateTime.Now.AddHours(3);
            //lead.StatusId = 1;
            //lead.DispId = 1;
            //lead.AgentId = 4;
            //lead.ClientId = 1;
            //lead.Dead = "Васильев Иван Фёдорович";
            //lead.Address = "Санкт-Петербург, ул. Фурштатская, д.30";

            //await db.Leads.AddAsync(lead);
            //await db.SaveChangesAsync();

            return Ok();
        }
    }
}
