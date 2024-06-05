using System.Text.Json.Serialization;

namespace FPLSP_Analyst.Application.ValueObjects.Common
{
    public class RefreshToken
    {
        [JsonPropertyName("user_id")]
        public Guid? UserId { get; set; } = null;
        [JsonPropertyName("token")]
        public string Token { get; set; } = string.Empty;
        [JsonPropertyName("created_time")]
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        [JsonPropertyName("expires")]
        public DateTime Expires { get; set; }
    }
}
