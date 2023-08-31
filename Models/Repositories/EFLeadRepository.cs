using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFLeadRepository : ILeadRepository
    {
        private PDBGContext _context;
        public EFLeadRepository(PDBGContext context)
        {
            _context = context;
        }

        public IQueryable<Lead> Leads => _context.Leads
            .Include(x => x.Agent)
            .Include(x => x.Disp)
            .Include(x => x.Status)
            .Include(x => x.Client);

        public async Task<List<Lead>> GetFiltredLeadsAsync(string strDateFrom, string strDateTo, int agentId, int dispId)
        {
            var leads = Leads;

            if (!string.IsNullOrWhiteSpace(strDateFrom) || !string.IsNullOrWhiteSpace(strDateTo))
            {
                DateTime dateFrom = DateTime.Parse(strDateFrom + "T00:00:00");
                DateTime dateTo = DateTime.Parse(strDateTo + "T23:59:59");
                leads = leads.Where(t => t.Created >= dateFrom && t.Created <= dateTo);
            }

            if (agentId != 0)
            {
                leads = leads.Where(t => t.AgentId == agentId);
            }

            if (dispId != 0)
            {
                leads = leads.Where(t => t.DispId == dispId);
            }

            return await leads.ToListAsync();
        }

        public async Task<Lead>? GetLeadByIdAsync(int id)
        {
            var lead = await Leads.FirstOrDefaultAsync(x => x.Id == id);
            return lead;
        }

        public async Task SaveLeadAsync(Lead lead)
        {
            var check = await Leads.FirstOrDefaultAsync(x => x.Id == lead.Id);

            if (check != null)
            {
                check.DispId = lead.DispId;
                check.AgentId = lead.AgentId;
                check.ClientId = lead.ClientId;
                check.Dead = lead.Dead;
                check.Address = lead.Address;
                check.Lat = lead.Lat;
                check.Lng = lead.Lng;
                check.NoteToAddress = lead.NoteToAddress;
                check.Comment = lead.Comment;
                check.Sum = lead.Sum;
                check.RejectionReason = lead.RejectionReason;
                _context.Leads.Update(check);
            }
            else
            {
                await _context.Leads.AddAsync(lead);
            }

            await _context.SaveChangesAsync();
        }
    }
}
