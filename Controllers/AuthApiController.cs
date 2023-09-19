using Microsoft.AspNetCore.Mvc;
using PDBG.CRM.WEB.Models.Repositories;

namespace PDBG.CRM.WEB.Controllers
{
	[Route("auth")]
	[ApiController]
	public class AuthApiController : ControllerBase
	{
		private IEmployeeAccessesRepository _employeeAccessesRepository;

		public AuthApiController(IEmployeeAccessesRepository employeeAccessesRepository)
		{
			_employeeAccessesRepository = employeeAccessesRepository;
		}

		public async Task<IActionResult> Index(string login, string password)
		{
			var agent = await _employeeAccessesRepository.CheckAccess(login, password);

			if (agent == null)
			{
				return NotFound();
			}

			return Ok(agent);
		}
	}
}
