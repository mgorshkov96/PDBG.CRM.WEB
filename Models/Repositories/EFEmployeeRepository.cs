using Microsoft.EntityFrameworkCore;

namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFEmployeeRepository : IEmployeeRepository
    {
        private PDBGContext _context;
        public EFEmployeeRepository(PDBGContext context)
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

        public async Task<Employee> UpdateEmployee(Employee employee)
        {
            var emp = await Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);

            if (emp != null)
            {
                emp.Name = employee.Name;
                emp.RoleId = employee.RoleId;
                emp.Phone = employee.Phone;
                emp.StatusId = employee.StatusId;
                emp.Access = employee.Access;
                _context.Employees.Update(emp);
                await _context.SaveChangesAsync();
			}

            return emp;
		}
    }
}
