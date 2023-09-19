using System.Text.Json;
using System.Text.Json.Serialization;

namespace PDBG.CRM.WEB.Models.JsonEntities
{
    public class AmoCustomField
    {
        [JsonPropertyName("field_id")]
        public int FieldId { get; set; }

        [JsonPropertyName("field_name")]
        public string? FieldName { get; set; }

        [JsonPropertyName("values")]
        public AmoCustomFieldValue[]? Values { get; set; }
    }

    public class AmoCustomFieldValue
    {
        [JsonPropertyName("value")]
        public string? Value { get; set; }

        [JsonPropertyName("enum_id")]
        public int? EnumId { get; set; }
    }
}
