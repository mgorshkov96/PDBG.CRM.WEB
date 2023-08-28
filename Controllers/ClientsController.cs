using Microsoft.AspNetCore.Mvc;
using PDBG.CRM.WEB.Models;

namespace PDBG.CRM.WEB.Controllers
{
    public class ClientsController : Controller
    {
        Models.AppContext db;
        public ClientsController(Models.AppContext context)
        {
            this.db = context;
        }

        public IActionResult Index()
        {
            return View(db.Clients);
        }
    }
}
