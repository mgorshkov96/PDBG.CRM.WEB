using System.Text.Json;
using System.Text.Json.Serialization;

namespace PDBG.CRM.WEB.Models.AmoEntities
{
    public class AmoLead
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("responsible_user_id")]
        public int ResponsibleUserId { get; set; }

        [JsonPropertyName("StatusId")]
        public int StatusId { get; set; }

        [JsonPropertyName("pipeline_id")]
        public int PipelineId { get; set; }

        [JsonPropertyName("custom_fields_values")]
        public AmoCustomField[]? CustomFieldsValues { get; set; }

        [JsonPropertyName("_embedded")]
        public AmoLeadEmbedded? Embedded { get; set; }
    }

    public class AmoLeadEmbedded
    {
        [JsonPropertyName("contacts")]
        public AmoContact[]? Contacts { get; set; }
    }
}
