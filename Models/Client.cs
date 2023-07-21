using System.ComponentModel.DataAnnotations.Schema;

namespace PDBG.CRM.WEB.Models
{
    [Table("t_clients")]
    public class Client
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Phone { get; set; }
    }
}
