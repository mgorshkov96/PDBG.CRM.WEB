namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFLocationLogRepository : ILocationLogRepository
    {
        private AppContext _context;
        public EFLocationLogRepository(AppContext context)
        {
            _context = context;
        }

        public IQueryable<LocationLog> LocationLogs => _context.LocationLogs;

        public async Task<LocationLog> AddLocationLogAsync(LocationLog locationLog)
        {
            await _context.LocationLogs.AddAsync(locationLog);
            await _context.SaveChangesAsync();
            return locationLog;
        }
    }
}
