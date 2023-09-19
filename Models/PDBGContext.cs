using Microsoft.EntityFrameworkCore;

namespace PDBG.CRM.WEB.Models
{
    public class PDBGContext : DbContext
    {
        public PDBGContext(DbContextOptions<PDBGContext> options)
            : base(options) 
        {
            //Database.EnsureCreated();
        }       

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LocationLog> LocationLogs { get; set; }
        public DbSet<AgentState> AgentStates { get; set; }
        public DbSet<Lead> Leads { get; set; }       
        public DbSet<AmoAuth> AmoAuthes { get; set; }
        public DbSet<LeadStatus> LeadStatuses { get; set; }
        public DbSet<AgentSearch> AgentSearches { get; set; }
        public DbSet<ApiKey> ApiKeys { get; set; }
		public DbSet<EmployeeAccess> Accesses { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AgentState>(a =>
            {
                a.HasNoKey();
                a.ToView("v_agent_state");
            });

			
		}
    }
}
