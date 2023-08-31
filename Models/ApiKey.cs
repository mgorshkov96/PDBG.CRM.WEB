using System.ComponentModel.DataAnnotations.Schema;

namespace PDBG.CRM.WEB.Models
{
    [Table("t_api_keys")]
    public class ApiKey
    {
        public int Id { get; set; }

        [Column("service_name")]
        public string ServiceName { get; set; }

        [Column("api_value")]
        public string ApiValue { get; set; }
        
        public ApiKey(int id, string serviceName, string apiValue) 
        {
            Id = id;
            ServiceName = serviceName;
            ApiValue = apiValue;
        }
    }
}
