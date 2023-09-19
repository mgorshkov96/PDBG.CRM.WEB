using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models;
using PDBG.CRM.WEB.Models.JsonEntities;
using PDBG.CRM.WEB.Models.Repositories;


namespace PDBG.CRM.WEB.Controllers
{
	[Route("amo/webhook")]
	[ApiController]
	public class AmoApiController : ControllerBase
	{
		private IAgentStateRepository _agentStateRepository;
		private IAgentSearchRepository _agentSearchRepository;
		private IAmoAuthRepository _amoAuthRepository;
		private IClientRepository _clientRepository;
		private IApiKeyRepository _apiKeyRepository;
		private ILeadRepository _leadRepository;
		//private AmoService _amoService;

		public AmoApiController(
			IAgentStateRepository agentStateRepository,
			IAgentSearchRepository agentSearchRepository,
			IAmoAuthRepository amoAuthRepository,
			IClientRepository clientRepository,
			IApiKeyRepository apiKeyRepository,
			ILeadRepository leadRepository//,
										  //AmoService amoService
			)
		{
			_agentStateRepository = agentStateRepository;
			_agentSearchRepository = agentSearchRepository;
			_amoAuthRepository = amoAuthRepository;
			_clientRepository = clientRepository;
			_apiKeyRepository = apiKeyRepository;
			_leadRepository = leadRepository;
			//_amoService = amoService;
		}

		[HttpPost]
		public async Task<IActionResult> CatchWebhook([FromForm] AmoWebhook webhook)
		{
			if (webhook == null || webhook.Leads == null)
			{
				return BadRequest();
			}

			AmoWebhookLead webhookLead = webhook.Leads.status[0];

			var caughtLead = await _leadRepository.GetLeadByIdAsync(webhook.Leads.status[0].id);

			AmoService amoSync = new AmoService(_amoAuthRepository);

			await amoSync.RequestLeadAndContactAsync(webhook.Leads.status[0].id);

			var amoLead = amoSync.Lead;
			var amoContact = amoSync.Contact;
			string phone = amoContact.GetPhone();
			Client client = new Client(amoContact.Id, amoContact.Name, phone);
			await _clientRepository.SaveAsync(client);
			var lead = new Lead(amoLead, amoContact.Id);

			if (caughtLead.Lat == null && caughtLead.Lng == null)
			{
				if (lead.Address != null)
				{
					YandexService yaMaps = new YandexService(_apiKeyRepository);
					string coords = await yaMaps.GetCoordsAsync(lead.Address);

					if (!String.IsNullOrEmpty(coords))
					{
						string[] coordinates = coords.Split(' ');
						decimal lng = Decimal.Parse(coordinates[0]);
						decimal lat = Decimal.Parse(coordinates[1]);

						lead.Lat = lat;
						lead.Lng = lng;
					}
				}
			}

			await _leadRepository.SaveLeadAsync(lead);

			if (lead.AgentId == null && lead.Lat != null && lead.Lng != null)
			{
				var distances = _agentStateRepository.GetNearest((decimal)lead.Lat, (decimal)lead.Lng, 3);

				if (distances != null)
				{
					foreach (var distance in distances)
					{
						var item = new AgentSearch(lead.Id, distance.Key, distance.Value);
						await _agentSearchRepository.AddAgentSearchAsync(item);
					}
				}
			}
			return Ok();
		}
	}
}

