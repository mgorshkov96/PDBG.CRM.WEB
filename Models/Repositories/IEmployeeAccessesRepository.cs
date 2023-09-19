namespace PDBG.CRM.WEB.Models.Repositories
{
	public interface IEmployeeAccessesRepository
	{
		IQueryable<EmployeeAccess> Accesses { get; }
		Task<Employee>? CheckAccess(string login, string password);
	}
}
