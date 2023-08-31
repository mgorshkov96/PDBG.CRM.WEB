namespace PDBG.CRM.WEB.Models.Repositories
{
    public interface IAgentStateRepository
    {
        IQueryable<AgentState> AgentStates { get; }
        IQueryable<AgentState> GetOnline();
        List<KeyValuePair<int, double>>? GetNearest(decimal latitude, decimal longitude, int maxCount);
    }
}
