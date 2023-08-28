using System.Text.Json;
using System.Text.Json.Serialization;

namespace PDBG.CRM.WEB.Models.AmoEntities
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
    }
}
