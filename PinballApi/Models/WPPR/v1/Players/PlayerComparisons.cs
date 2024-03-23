using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class PlayerComparisons
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("total_competitors")]
        public int TotalCompetitors { get; set; }

        [JsonPropertyName("pvp")]
        public List<PlayerVersusRecord> Pvp { get; set; }
    }
}
