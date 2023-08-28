using Microsoft.EntityFrameworkCore;
using PDBG.CRM.WEB.Models.AmoEntities;

namespace PDBG.CRM.WEB.Models
{
    public class AppContext : DbContext
    {
        public AppContext(DbContextOptions<AppContext> options)
            : base(options) 
        {
            //Database.EnsureCreated();
        }       

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LocationLog> LocationLogs { get; set; }
        public DbSet<AgentState> AgentStates { get; set; }
        public DbSet<Lead> Leads { get; set; }       
        public DbSet<AmoAuth> AmoAuths { get; set; }
        public DbSet<LeadStatus> LeadStatuses { get; set; }

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
