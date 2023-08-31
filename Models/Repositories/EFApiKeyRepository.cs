namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFApiKeyRepository : IApiKeyRepository
    {
        private PDBGContext _context;

        public EFApiKeyRepository(PDBGContext context)
        {
            _context = context;
        }

        public IQueryable<ApiKey> ApiKeys => _context.ApiKeys;

        public string GetApiKeyByName(string serviceName)
        {
            var apiKey = ApiKeys.FirstOrDefault(x => x.ServiceName == serviceName);

            if (apiKey == null)
            {
                return "";
            }

            return apiKey.ApiValue;
        }
    }
}
