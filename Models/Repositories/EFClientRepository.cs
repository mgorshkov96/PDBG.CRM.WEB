namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFClientRepository : IClientRepository
    {
        private PDBGContext _context;
        public EFClientRepository(PDBGContext context)
        {
            _context = context;
        }

        public IQueryable<Client> Clients => _context.Clients;

        public async Task SaveAsync(Client client)
        {
            var check = Clients.FirstOrDefault(x => x.Id == client.Id);

            if (check == null)
            {
                await _context.Clients.AddAsync(client);
            }

            await _context.SaveChangesAsync();
        }
    }
}
