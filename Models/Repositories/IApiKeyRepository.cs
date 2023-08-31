namespace PDBG.CRM.WEB.Models.Repositories
{
    public interface IApiKeyRepository
    {
        IQueryable<ApiKey> ApiKeys { get; }
        string GetApiKeyByName(string serviceName);
    }
}
