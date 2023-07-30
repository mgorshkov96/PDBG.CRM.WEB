using Microsoft.AspNetCore.Mvc.Rendering;

namespace PDBG.CRM.WEB.Models
{
    public class LeadsFilterViewModel
    {
        public LeadsFilterViewModel(List<Employee> disps, int disp, List<Employee> agents, int agent, /*int leadId, */string dateFrom, string dateTo) 
        {
            disps.Insert(0, new Employee { Name = "Все", Id = 0 });
            agents.Insert(0, new Employee { Name = "Все", Id = 0 });
            Disps = new SelectList(disps, "Id", "Name", disp);
            Agents = new SelectList(agents, "Id", "Name", agents);
            SelectedDisp = disp;
            SelectedAgent = agent;
            //EnteredLeadId = leadId;
            EnteredDateFrom = dateFrom;
            EnteredDateTo = dateTo;
        }

        public SelectList Disps { get; }
        public SelectList Agents { get; }
        public int SelectedDisp { get; }
        public int SelectedAgent { get; }
        //public int EnteredLeadId { get; }
        public string EnteredDateFrom { get; }
        public string EnteredDateTo { get; }
    }
}
