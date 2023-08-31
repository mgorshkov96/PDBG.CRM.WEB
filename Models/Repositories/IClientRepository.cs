namespace PDBG.CRM.WEB.Models.Repositories
{
    public interface IClientRepository
    {
        IQueryable<Client> Clients { get; }
        Task SaveAsync(Client client);
    }
}
