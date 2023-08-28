using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDBG.CRM.WEB.Models
{
    [Table("t_employees")]
    public class Employee
    {
        public int Id { get; set; }

        [Column("amo_id")]
        public int? AmoId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }
        
        public string Name { get; set; }

        public string? Phone { get; set; }

        [Column("status_id")]
        public int? StatusId { get; set; }
       
        public int Access { get; set; }
    }
}
