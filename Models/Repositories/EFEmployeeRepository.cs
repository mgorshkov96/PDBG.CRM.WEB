using Microsoft.EntityFrameworkCore;

namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private AppContext _context;
        public EFEmployeeRepository(AppContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> Employees => _context.Employees;

        public async Task<List<Employee>> GetAgentsAsync()
        {
            var agents = await (from d in Employees
                          where d.RoleId == 2
                          select d).ToListAsync();
            return agents;
        }

        public async Task<List<Employee>> GetDispsAsync()
        {
            var agents = await (from d in Employees
                          where d.RoleId == 1
                          select d).ToListAsync();
            return agents;
        }
    }
}
