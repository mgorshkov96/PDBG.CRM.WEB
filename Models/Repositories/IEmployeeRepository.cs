namespace PDBG.CRM.WEB.Models.Repositories
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> Employees { get; }
        Task<List<Employee>> GetAgentsAsync();
        Task<List<Employee>> GetDispsAsync();
    }
}
