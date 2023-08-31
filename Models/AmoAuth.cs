using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PDBG.CRM.WEB.Models
{
    [Table("t_amo_auth")]
    public class AmoAuth
    {
        public int Id { get; set; }

        [Column("client_id")]
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [Column("client_secret")]
        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }

        [Column("grant_type")]
        [JsonPropertyName("grant_type")]
        public string? GrantType { get; set; }

        [Column("code")]
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [Column("refresh_token")]
        [JsonPropertyName("refresh_token")]
        public string? RefreshToken { get; set; }

        [Column("access_token")]
        [JsonPropertyName("access_token")]
        public string? AccessToken { get; set; }

        [Column("redirect_uri")]
        [JsonPropertyName("redirect_uri")]
        public string RedirectUri { get; set; }
    }
}
