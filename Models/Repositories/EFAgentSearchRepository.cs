using Microsoft.EntityFrameworkCore;

namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFAgentSearchRepository : IAgentSearchRepository
    {
        private PDBGContext _context;

        public EFAgentSearchRepository(PDBGContext context)
        {
            _context = context;
        }

        public IQueryable<AgentSearch> AgentSearches => _context.AgentSearches
            .Include(x => x.Lead);

        public async Task AddAgentSearchAsync(AgentSearch agentSearch)
        {
            var item = await AgentSearches.FirstOrDefaultAsync(x => x.LeadId == agentSearch.LeadId);

            if (item == null)
            {
                await _context.AgentSearches.AddAsync(agentSearch);
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAgentSearchesAsync(int leadId)
        {
            var searches = await AgentSearches.Where(x => x.LeadId == leadId).ToListAsync();


            _context.AgentSearches.RemoveRange(searches);
            await _context.SaveChangesAsync();
        }

	}
}
