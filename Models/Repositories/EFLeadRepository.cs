using Microsoft.EntityFrameworkCore;

namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFLeadRepository : ILeadRepository
    {
        private AppContext _context;
        public EFLeadRepository(AppContext context)
        {
            _context = context;
        }

        public IQueryable<Lead> Leads => _context.Leads
            .Include(x => x.Agent)
            .Include(x => x.Disp)
            .Include(x => x.Status)
            .Include(x => x.Client);

        public async Task<List<Lead>> GetFiltredLeadsAsync(string strDateFrom, string strDateTo, int agentId, int dispId)
        {
            var leads = Leads;

            if (!string.IsNullOrWhiteSpace(strDateFrom) || !string.IsNullOrWhiteSpace(strDateTo))
            {
                DateTime dateFrom = DateTime.Parse(strDateFrom + "T00:00:00");
                DateTime dateTo = DateTime.Parse(strDateTo + "T23:59:59");
                leads = leads.Where(t => t.Created >= dateFrom && t.Created <= dateTo);
            }

            if (agentId != 0)
            {
                leads = leads.Where(t => t.AgentId == agentId);
            }

            if (dispId != 0)
            {
                leads = leads.Where(t => t.DispId == dispId);
            }

            return await leads.ToListAsync();
        }

    }
}
