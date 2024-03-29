﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PDBG.CRM.WEB.Models;

namespace PDBG.CRM.WEB.Controllers
{
	[Authorize]
	public class ClientsController : Controller
    {
        Models.PDBGContext db;
        public ClientsController(Models.PDBGContext context)
        {
            this.db = context;
        }

        public IActionResult Index()
        {
            return View(db.Clients);
        }
    }
}
