using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models;
using PDBG.CRM.WEB.Models.Repositories;
using System.Text.Json;

namespace PDBG.CRM.WEB.Controllers
{
    [ApiController]
    [Route("api/leads")]
    public class LeadsApiController : ControllerBase
    {
        private ILeadRepository _leadRepository;
        private IAgentSearchRepository _agentSearchRepository;
        private IAmoAuthRepository _amoAuthRepository;

        public LeadsApiController(ILeadRepository leadRepository, IAgentSearchRepository agentSearchRepository, IAmoAuthRepository amoAuthRepository)
        {
            _leadRepository = leadRepository;
            _agentSearchRepository = agentSearchRepository;
			_amoAuthRepository = amoAuthRepository;
        }       

		[Route("inwork")]
		public async Task<IActionResult> GetLeadsInWork(int agentId)
        {
            var leads = _leadRepository.Leads;
            leads = leads.Where(x => x.AgentId == agentId && x.StatusId == 1);

            if (leads != null)
            {
                return Ok(await leads.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

		[Route("near")]
		public async Task<IActionResult> GetNearLeads(int agentId)
        {
            var leads = _agentSearchRepository.AgentSearches;
            leads = leads.Where(x => x.AgentId == agentId);

			if (leads != null)
            {
                return Ok(await leads.ToListAsync());
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveLead(Lead lead)
        {
            await _leadRepository.SaveLeadAsync(lead);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetLeadById(int id)
        {
            var lead = await _leadRepository.Leads.FirstOrDefaultAsync(x => x.Id == id);

            if (lead == null)
            {
                return NotFound();
            }

            return Ok(lead);
        }

        [Route("appoint")]
		[HttpGet]
        public async Task<IActionResult> AppointAnAgent(int agentId, int leadId)
        {
            var lead = _leadRepository.Leads.FirstOrDefault(x => x.Id == leadId);

            if (lead == null)
            {
				return BadRequest("Произошла ошибка");
			}

            if (lead.AgentId == null)
            {
				lead.AgentId = agentId;
				await _leadRepository.SaveLeadAsync(lead);
				await _agentSearchRepository.DeleteAgentSearchesAsync(leadId);
                return Ok(lead);
			}
            else
            {
				await _agentSearchRepository.DeleteAgentSearchesAsync(agentId);
				return BadRequest("Агент уже назначен");
			}			
		}

        [HttpPut]
        public async Task<IActionResult> UpdateLead(Lead lead)
        {
			AmoService amoSync = new AmoService(_amoAuthRepository);
            var isSaveSuccess = await amoSync.SaveLeadAsync(lead);
            //var test = await amoSync.SaveLeadAsync(lead);
            if (!isSaveSuccess)
                return BadRequest();

            await _leadRepository.SaveLeadAsync(lead);
            return Ok(lead);           
        }
    }
}
