using System.ComponentModel.DataAnnotations.Schema;

namespace PDBG.CRM.WEB.Models
{
    public class ViewLead
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }        
        public string? Status { get; set; }
        public string? Disp { get; set; }
        public string? Agent { get; set; }
        public string? Dead { get; set; }
        public string? Address { get; set; }

        [Column("note_to_address")]
        public string? NoteToAddress { get; set; }

        public string? Comment { get; set; }
        public decimal Sum { get; set; }

        [Column("rejection_reason")]
        public string? RejectionReason { get; set; }
    }
}
