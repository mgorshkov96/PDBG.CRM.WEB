using Microsoft.AspNetCore.Mvc;
using PDBG.CRM.WEB.Models;

namespace PDBG.CRM.WEB.Controllers
{
    public class ClientsController : Controller
    {
        MyContext db;
        public ClientsController(MyContext context)
        {
            this.db = context;
        }

        public IActionResult Index()
        {
            return View(db.Clients);
        }
    }
}
