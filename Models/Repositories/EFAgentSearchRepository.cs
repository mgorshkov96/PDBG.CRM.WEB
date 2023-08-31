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

        public IQueryable<AgentSearch> AgentSearches => _context.AgentSearches;

        public async Task AddAgentSearchAsync(AgentSearch agentSearch)
        {
            var item = await AgentSearches.FirstOrDefaultAsync(x => x.AgentId == agentSearch.AgentId);

            if (item == null)
            {
                await _context.AgentSearches.AddAsync(agentSearch);
            }

            await _context.SaveChangesAsync();
        }
    }
}
