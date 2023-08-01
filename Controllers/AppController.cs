using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models;
using System.Diagnostics;

namespace PDBG.CRM.WEB.Controllers
{
    public class AppController : Controller
    {
        private readonly ILogger<AppController> _logger;

        private MyContext db;

        public AppController(ILogger<AppController> logger, MyContext context)
        {
            _logger = logger;
            this.db = context;
        }

        public IActionResult AgentsOnMap()
        {
            return View();
        }

        public async Task<IActionResult> Leads(string dateFrom, string dateTo, int agent = 0, int disp = 0, int page = 1)
        {
            if (String.IsNullOrEmpty(dateFrom) || String.IsNullOrEmpty(dateTo)) 
            {
                dateFrom = DateTime.Now.AddHours(3).ToString("yyyy-MM-dd");
                dateTo = DateTime.Now.AddHours(3).ToString("yyyy-MM-dd");
            }

            var leads = await db.getFiltredLeadsAsync(dateFrom, dateTo, agent, disp);            

            // пагинация
            int pageSize = 25;

            var count = leads.Count();
            var items = leads.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // модель представления
            var agents = await db.GetAgentsAsync();
            var disps = await db.GetDispsAsync();

            LeadsViewModel leadsViewModel = new LeadsViewModel(
                items,
                new LeadsPageViewModel(count, page, pageSize),
                new LeadsFilterViewModel(disps, disp, agents, agent, dateFrom, dateTo)
            );

            return View(leadsViewModel);
        }
        
        public async Task<IActionResult> Lead(int id) 
        {
            var lead = await db.ViewLeads.FirstOrDefaultAsync(x => x.Id == id);

            if (lead == null)
            {
                return NotFound();
            }

			return View(lead);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}