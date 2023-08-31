using PDBG.CRM.WEB.Models.JsonEntities;
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
        public int? AgentId { get; set; }

        [Column("client_id")]
        public int ClientId { get; set; }

        public string? Dead { get; set; }

        public string? Address { get; set; }

        public decimal? Lat { get; set; }

        public decimal? Lng { get; set; }

        [Column("note_to_address")]
        public string? NoteToAddress { get; set; }

        public string? Comment { get; set; }

        public decimal? Sum { get; set; }

        [Column("rejection_reason")]
        public string? RejectionReason { get; set; }

        public Employee? Disp { get; set; }
        public Employee? Agent { get; set; }
        public LeadStatus? Status { get; set; }
        public Client? Client { get; set; }

        public Lead(int id, int dispId, int clientId)
        {
            Id = id;
            Created = DateTime.Now.AddHours(3);
            StatusId = 1;
            DispId = dispId;
            ClientId = clientId;            
        }

        public Lead()
        {            
            Created = DateTime.Now.AddHours(3);
            StatusId = 1;
        }

        public Lead(AmoLead amoLead, int clientId)
        {
            Id = amoLead.Id;
            Created = DateTime.Now.AddHours(3);
            StatusId = 1;
            DispId = amoLead.ResponsibleUserId;
            ClientId = clientId;
            Sum = 0;

            foreach (var item in amoLead.CustomFieldsValues)
            {
                switch (item.FieldId)
                {                   
                    case 648113:
                        Comment = item.Values[0].Value;
                        break;
                    case 648279:
                        Address = item.Values[0].Value;
                        break;
                    case 648355:
                        Dead = item.Values[0].Value;
                        break;                  
                    case 859127:
                        AgentId = item.Values[0].EnumId;
                        break;
                    case 866055:
                        NoteToAddress = item.Values[0].Value;
                        break;
                }
            }
        }
    }
}
