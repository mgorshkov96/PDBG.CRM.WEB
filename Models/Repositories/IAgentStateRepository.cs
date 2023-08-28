namespace PDBG.CRM.WEB.Models.Repositories
{
    public interface IAgentStateRepository
    {
        IQueryable<AgentState> AgentStates { get; }
    }
}
