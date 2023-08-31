using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace PDBG.CRM.WEB.Models
{
    [PrimaryKey(nameof(LeadId), nameof(AgentId))]
    [Table("t_agent_search")]
    public class AgentSearch
    {
        [Column("lead_id")]
        public int LeadId { get; set; }

        [Column("agent_id")]
        public int AgentId { get; set; }

        [Column("search_time")]
        public DateTime SearchTime { get; set; }

        public double Distance { get; set; }

        public AgentSearch(int leadId, int agentId, double distance) 
        {
            LeadId = leadId;
            SearchTime = DateTime.Now.AddHours(3);
            AgentId = agentId;
            Distance = distance;
        }
    }
}
