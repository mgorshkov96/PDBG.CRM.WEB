using Microsoft.AspNetCore.Mvc;
using PDBG.CRM.WEB.Models;
using System.Diagnostics;

namespace PDBG.CRM.WEB.Controllers
{
    public class AppController : Controller
    {
        private readonly ILogger<AppController> _logger;

        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
        }

        public IActionResult Map()
        {
            return View();
        }

        public IActionResult Leads()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}