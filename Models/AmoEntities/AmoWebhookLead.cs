namespace PDBG.CRM.WEB.Models.AmoEntities
{
    public class AmoWebhookLead
    {
        public int id { get; set; }

        public int old_pipeline_id { get; set; }

        public int pipeline_id { get; set; }

        public int old_status_id { get; set; }

        public int status_id { get; set; }
    }
}
