using Microsoft.AspNetCore.Mvc;
using PDBG.CRM.WEB.Models;
using PDBG.CRM.WEB.Models.Repositories;

namespace PDBG.CRM.WEB.Controllers
{
	[Route("api/employees")]
	[ApiController]
	public class EmployeeApiController : Controller
	{
		private IEmployeeRepository _employeeRepository;

		public EmployeeApiController(IEmployeeRepository employeeRepository)
		{
			_employeeRepository = employeeRepository;
		}

		[HttpPut]
		public IActionResult ChangeEmployee(Employee employee)
		{
			var emp = _employeeRepository.UpdateEmployee(employee);

			if (emp == null)
			{
				return NotFound();
			}

			return Ok(emp);
		}
	}
}
