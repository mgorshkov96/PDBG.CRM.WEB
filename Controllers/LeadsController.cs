using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models.Repositories;
using PDBG.CRM.WEB.Models.ViewModels;

namespace PDBG.CRM.WEB.Controllers
{
    public class LeadsController : Controller
    {
        private ILeadRepository _leadRepository;
        private IEmployeeRepository _employeeRepository;

        public LeadsController(ILeadRepository leadRepository, IEmployeeRepository employeeRepository)
        {
            _leadRepository = leadRepository;
            _employeeRepository = employeeRepository;
        }

        [Route("Leads/List")]
        public async Task<ViewResult> List(string dateFrom, string dateTo, int agent = 0, int disp = 0, int page = 1)
        {
            if (String.IsNullOrEmpty(dateFrom) || String.IsNullOrEmpty(dateTo))
            {
                dateFrom = DateTime.Now.AddHours(3).ToString("yyyy-MM-dd");
                dateTo = DateTime.Now.AddHours(3).ToString("yyyy-MM-dd");
            }

            var leads = await _leadRepository.GetFiltredLeadsAsync(dateFrom, dateTo, agent, disp);

            // пагинация
            int pageSize = 25;

            var count = leads.Count();
            var items = leads.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // модель представления
            var agents = await _employeeRepository.GetAgentsAsync();
            var disps = await _employeeRepository.GetDispsAsync();

            LeadsViewModel leadsViewModel = new LeadsViewModel(
            items,
                new LeadsPageViewModel(count, page, pageSize),
                new LeadsFilterViewModel(disps, disp, agents, agent, dateFrom, dateTo)
            );

            return View(leadsViewModel);
        }

		[Route("Leads/{id}")]
		public async Task<ViewResult> Lead(int id)
        {
            var lead = await _leadRepository.Leads.FirstOrDefaultAsync(x => x.Id == id);

            if (lead == null)
            {
                return View(NotFound());
            }

            return View(lead);
        }
    }
}
