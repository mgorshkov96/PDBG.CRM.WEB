using Microsoft.CodeAnalysis.Elfie.Serialization;

namespace PDBG.CRM.WEB.Models.Repositories
{
    public class EFAgentStateRepository : IAgentStateRepository
    {
        private PDBGContext _context;
        public EFAgentStateRepository(PDBGContext context)
        {
            _context = context;
        }
        public IQueryable<AgentState> AgentStates => _context.AgentStates;

        public IQueryable<AgentState> GetOnline()
        {
            var onlineAgents = from agent in AgentStates
                               where agent.StatusName == "Онлайн"
                               select agent;
            return onlineAgents;
        }

        public List<KeyValuePair<int, double>>? GetNearest(decimal latitude, decimal longitude, int maxCount)
        {
            var agentsOnline = GetOnline().ToList();

            if (agentsOnline == null)
            {
                return null;
            }

            var distances = new Dictionary<int, double>();

            double x1 = Decimal.ToDouble((decimal)latitude);
            double y1 = Decimal.ToDouble((decimal)longitude);

            foreach (var item in agentsOnline)
            {
                double x2 = Decimal.ToDouble(item.Lat);
                double y2 = Decimal.ToDouble(item.Lng);
                double dist = Math.Sqrt(Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
                distances.Add(item.AgentId, dist);
            }

            var sortedDistances = distances.OrderBy(x => x.Value).ToList();
            var result = new List<KeyValuePair<int, double>>();

            for (int i = 0; i < maxCount; i++)
            {
                if (i < sortedDistances.Count)
                {
                    result.Add(sortedDistances[i]);
                }
                else
                {
                    break;
                }
            }
            return result;
        }
    }
}
