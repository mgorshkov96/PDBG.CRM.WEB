using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDBG.CRM.WEB.Models
{
	[Table("t_employee_accesses")]	
	public class EmployeeAccess
	{
		[Key]
		[ForeignKey("Agent")]	
		public int? Id { get; set; }		
		public string Login { get; set; }
		public  string Password { get; set; }

		public Employee? Agent { get; set; }
	}
}
