namespace PDBG.CRM.WEB.Models.Repositories
{
    public interface ILeadRepository
    {
        IQueryable<Lead> Leads { get; }
        Task<List<Lead>> GetFiltredLeadsAsync(string strDateFrom, string strDateTo, int agentId, int dispId);
        Task<Lead>? GetLeadByIdAsync(int id);
        Task SaveLeadAsync(Lead lead);
    }
}
