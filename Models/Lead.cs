using System.ComponentModel.DataAnnotations.Schema;

namespace PDBG.CRM.WEB.Models
{
    [Table("t_leads")]
    public class Lead
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }

        [Column("status_id")]
        public int StatusId { get; set; }

        [Column("disp_id")]
        public int DispId { get; set; }

        [Column("agent_id")]
        public int AgentId { get; set; }

        [Column("client_id")]
        public int ClientId { get; set; }

        public string? Dead { get; set; }

        public string? Address { get; set; }

        public decimal? Lat { get; set; }

        public decimal? Lng { get; set; }

        [Column("note_to_address")]
        public string? NoteToAddress { get; set; }

        public string? Comment { get; set; }

        public decimal Sum { get; set; }

        [Column("rejection_reason")]
        public string? RejectionReason { get; set; }

        public Employee? Disp { get; set; }
        public Employee? Agent { get; set; }
    }
}
