using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class CurrentLeader
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("player_name")]
        public string PlayerName { get; set; }
    }
}
