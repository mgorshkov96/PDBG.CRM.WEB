using PDBG.CRM.WEB.Models.AmoEntities;

namespace PDBG.CRM.WEB.Models
{
    public class AmoSync
    {
        private AppContext db;

        private int leadId;

        public AmoLead? AmoLead { get; set; }

        public AmoContact? AmoContact { get; set; }

        public Lead? Lead { get; set; }

        public Client? Client { get; set; }

        public AmoSync(AppContext context, int leadId)
        {
            this.db = context;
            this.leadId = leadId;
        }

        public bool Synchronize()
        {
            return true;
        }

        private void FillLead()
        {
            var disp = db.Employees.FirstOrDefault(e => e.AmoId == AmoLead.ResponsibleUserId);

            if (disp != null)
            {
                Lead = new Lead(
                    AmoLead.Id,
                    disp.Id,
                    Client.Id);

                foreach (var item in AmoLead.CustomFieldsValues)
                {
                    switch (item.FieldId)
                    {
                        case 648355:
                            Lead.Dead = item.Values[0].Value;
                            break;
                        case 648113:
                            Lead.Comment = item.Values[0].Value;
                            break;
                        case 648279:
                            Lead.Address = item.Values[0].Value;
                            break;
                        case 866055:
                            Lead.NoteToAddress = item.Values[0].Value;
                            break;
                    }
                }
            }
        }

        private void FillClient()
        {
            Client.Id = AmoContact.Id;
            Client.Name = AmoContact.Name;
            string phone;

            foreach (var item in AmoContact.CustomFieldsValues)
            {
                if (String.Equals(item.FieldName, "Телефон"))
                {
                    phone = item.Values.FirstOrDefault().Value;
                    Client.Phone = phone;
                }
            }           
        }
    }
}
