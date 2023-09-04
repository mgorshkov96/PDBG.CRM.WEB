using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PDBG.CRM.WEB.Controllers
{
    [Authorize]
	public class MapController : Controller
	{        
        public ViewResult AgentsOnMap()
        {
            return View();
        }
    }
}
