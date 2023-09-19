using System.Text.Json.Serialization;

namespace PDBG.CRM.WEB.Models.JsonEntities
{
	public class AmoOutputLead
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("status_id")]
		public int StatusId { get; set; }

		[JsonPropertyName("pipeline_id")]
		public int PipelineId { get; set; }

		[JsonPropertyName("price")]
		public int Price { get; set; }

		[JsonPropertyName("custom_fields_values")]
		public List<AmoOutputCustomField>? CustomFieldsValues { get; set; }

		public AmoOutputLead(int id)
		{
			Id = id;
			PipelineId = 7084610;
		}
	}

	public class AmoOutputCustomField
	{
		[JsonPropertyName("field_id")]
		public int FieldId { get; set; }

		[JsonPropertyName("field_name")]
		public string? FieldName { get; set; }

		[JsonPropertyName("values")]
		public AmoOutputCustomFieldValue[]? Values { get; set; }
	}

	public class AmoOutputCustomFieldValue
	{
		[JsonPropertyName("value")]
		public string? Value { get; set; }		
	}
}
