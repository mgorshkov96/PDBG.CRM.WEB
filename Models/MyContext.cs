using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace PDBG.CRM.WEB.Models
{
    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options)
            : base(options) 
        {
            //Database.EnsureCreated();
        }       

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LocationLog> LocationLogs { get; set; }
        public DbSet<AgentState> AgentStates { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<ViewLead> ViewLeads { get; set; }

        public async Task<IEnumerable<ViewLead>> getFiltredLeadsAsync(string strDateFrom, string strDateTo, int agentId, int dispId)
        {
            var leads = from ld in ViewLeads
                        select ld;

            if (!string.IsNullOrWhiteSpace(strDateFrom) || !string.IsNullOrWhiteSpace(strDateTo))
            {
                DateTime dateFrom = DateTime.Parse(strDateFrom + "T00:00:00");
                DateTime dateTo = DateTime.Parse(strDateTo + "T23:59:59");               
                leads = leads.Where(t => t.Created >= dateFrom && t.Created <= dateTo);
            }

            if (agentId != 0) 
            { 
                Employee? agent = await Employees.FirstOrDefaultAsync(x => x.Id == agentId);
                leads = leads.Where(t => t.Agent == agent.Name);
            }

            if (dispId != 0)
            {
                Employee? disp = await Employees.FirstOrDefaultAsync(x => x.Id == dispId);
                leads = leads.Where(t => t.Disp == disp.Name);
            }

            var result = await leads.ToListAsync();

            return result;                       
        }

        public async Task<List<Employee>> GetDispsAsync()
        {
            var disps = await (from d in Employees
                        where d.RoleId == 1
                        select d).ToListAsync();
            return disps;
        }

        public async Task<List<Employee>> GetAgentsAsync()
        {
            var agents = await (from d in Employees
                         where d.RoleId == 2
                         select d).ToListAsync();
            return agents;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AgentState>(a =>
            {
                a.HasNoKey();
                a.ToView("v_agent_state");
            });

            modelBuilder.Entity<ViewLead>(a =>
            {
                a.HasNoKey();
                a.ToView("v_leads");
            });

            //modelBuilder.Entity<LocationLog>()
            //    .HasOne(e => e.Employee);
        }
    }
}
