namespace PDBG.CRM.WEB.Models.JsonEntities
{
    public class AmoWebhook
    {
        public AmoWebhookBody Leads { get; set; }
    }

    public class AmoWebhookBody
    {
        public AmoWebhookLead[] status { get; set; }
    }
}
