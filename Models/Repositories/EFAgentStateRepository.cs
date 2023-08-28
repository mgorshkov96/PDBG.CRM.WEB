namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFAgentStateRepository : IAgentStateRepository
    {
        private AppContext _context;
        public EFAgentStateRepository(AppContext context)
        {
            _context = context;
        }
        public IQueryable<AgentState> AgentStates => _context.AgentStates;
    }
}
