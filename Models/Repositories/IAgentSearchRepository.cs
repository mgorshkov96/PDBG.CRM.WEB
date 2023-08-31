using System.Runtime.CompilerServices;

namespace PDBG.CRM.WEB.Models.Repositories
{
    public interface IAgentSearchRepository
    {
        IQueryable<AgentSearch> AgentSearches { get; }
        Task AddAgentSearchAsync(AgentSearch agentSearch);
    }
}
