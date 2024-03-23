using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class PlayerRecord
    {
        [JsonPropertyName("player")]
        public Player Player { get; set; }

        [JsonPropertyName("player_stats")]
        public PlayerStats PlayerStats { get; set; }

    }
}
