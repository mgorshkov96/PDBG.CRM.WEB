namespace PDBG.CRM.WEB.Models.Repositories
{
    public interface ILocationLogRepository
    {
        IQueryable<LocationLog> LocationLogs { get; }
        Task<LocationLog> AddLocationLogAsync(LocationLog locationLog);
    }
}
