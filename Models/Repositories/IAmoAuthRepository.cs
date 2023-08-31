namespace PDBG.CRM.WEB.Models.Repositories
{
    public interface IAmoAuthRepository
    {
        IQueryable<AmoAuth> AmoAuthes { get; }
        //Task<string> GetNewAccessTokenAsync();
        Task UpdateAuthAsync(AmoAuth amoAuth);
    }
}
