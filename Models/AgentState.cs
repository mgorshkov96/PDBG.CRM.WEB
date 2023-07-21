using System.ComponentModel.DataAnnotations.Schema;

namespace PDBG.CRM.WEB.Models
{
    public class AgentState
    {
        [Column("agent_id")]
        public int AgentId { get; set; }

        [Column("agent_name")]
        public string AgentName { get; set; }

        [Column("status_name")]
        public string StatusName { get; set; }

        [Column("l_date")]
        public DateTime LDate { get; set; }

        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
    }
}
