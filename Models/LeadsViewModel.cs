namespace PDBG.CRM.WEB.Models
{
    public class LeadsViewModel
    {
        public IEnumerable<ViewLead> Leads { get; }
        public LeadsPageViewModel LeadsPageViewModel { get; }
        public LeadsFilterViewModel LeadsFilterViewModel { get; }

        public LeadsViewModel(IEnumerable<ViewLead> leads, LeadsPageViewModel leadsPageViewModel, LeadsFilterViewModel leadsFilterViewModel)
        {
            Leads = leads;
            LeadsPageViewModel = leadsPageViewModel;
            LeadsFilterViewModel = leadsFilterViewModel;
        }
    }
}
