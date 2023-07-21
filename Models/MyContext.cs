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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AgentState>(a =>
            {
                a.HasNoKey();
                a.ToView("v_agent_state");
            });

            //modelBuilder.Entity<LocationLog>()
            //    .HasOne(e => e.Employee);
        }
    }
}
