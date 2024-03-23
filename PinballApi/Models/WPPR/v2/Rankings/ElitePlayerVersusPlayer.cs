using System.Text.Json.Serialization;
using PinballApi.Models.WPPR.v2.Players;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class ElitePlayerVersusPlayer
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("total_wins")]
        public int TotalWins { get; set; }

        [JsonPropertyName("total_losses")]
        public int TotalLosses { get; set; }

        [JsonPropertyName("total_draws")]
        public int TotalDraws { get; set; }

        [JsonPropertyName("pvp")]
        public List<PlayerVersusRecord> Records { get; set; }
    }
}
