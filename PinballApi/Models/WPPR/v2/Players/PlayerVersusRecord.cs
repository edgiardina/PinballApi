using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerVersusRecord
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("win_count")]
        public int WinCount { get; set; }

        [JsonPropertyName("loss_count")]
        public int LossCount { get; set; }

        [JsonPropertyName("tie_count")]
        public int TieCount { get; set; }
    }
}
