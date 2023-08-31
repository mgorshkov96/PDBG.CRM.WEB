using System.Text.Json;
using System.Text.Json.Serialization;

namespace PDBG.CRM.WEB.Models.JsonEntities
{
    public class AmoContact
    {
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("is_main")]
        public bool? IsMain { get; set; }

        [JsonPropertyName("custom_fields_values")]
        public AmoCustomField[]? CustomFieldsValues { get; set; }

        public string GetPhone()
        {
            string amoPhone;

            foreach (var item in this.CustomFieldsValues)
            {
                if (item.FieldId == 607465)
                {
                    amoPhone = item.Values[0].Value;
                    return amoPhone; ;
                }
            }

            return null;
        }
    }
}
