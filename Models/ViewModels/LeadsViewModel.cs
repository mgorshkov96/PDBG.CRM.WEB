namespace PDBG.CRM.WEB.Models.ViewModels
{
    public class LeadsViewModel
    {
        public IEnumerable<Lead> Leads { get; }
        public LeadsPageViewModel LeadsPageViewModel { get; }
        public LeadsFilterViewModel LeadsFilterViewModel { get; }

        public LeadsViewModel(IEnumerable<Lead> leads, LeadsPageViewModel leadsPageViewModel, LeadsFilterViewModel leadsFilterViewModel)
        {
            Leads = leads;
            LeadsPageViewModel = leadsPageViewModel;
            LeadsFilterViewModel = leadsFilterViewModel;
        }
    }
}
