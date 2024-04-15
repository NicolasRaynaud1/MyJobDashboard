using System.Text.Json.Serialization;

namespace MyJobDashboard.Models
{
    public class AccessToken
    {
        [JsonPropertyName("access_token")]
        public required string TokenString { get; set; }

        [JsonPropertyName("scope")]
        public required string Scope { get; set; }

        [JsonPropertyName("expires_in")]
        public required int Expiration { get; set; }

        [JsonPropertyName("token_type")]
        public required string TokenType { get; set; }
    }
}
