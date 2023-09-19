using Microsoft.EntityFrameworkCore;

namespace PDBG.CRM.WEB.Models.Repositories
{
	public class EFEmployeeAccessesRepository : IEmployeeAccessesRepository
	{
		private PDBGContext _context;

		public EFEmployeeAccessesRepository(PDBGContext context)
		{
			_context = context;
		}

		public IQueryable<EmployeeAccess> Accesses => _context.Accesses
			.Include(x => x.Agent);

		public async Task<Employee>? CheckAccess(string login, string password)
		{
			var agent = await Accesses.FirstOrDefaultAsync(x => x.Login == login/*String.Equals(x.Login, login)*/);

			if (agent == null)
			{
				return null;
			}

			if (!String.Equals(agent.Password, password))
			{
				return null;
			}

			return agent.Agent;
		}
	}
}
