using System.Text.Json.Serialization;
using System.Collections.Generic;
using PinballApi.Converters;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class PlayerVersusPlayer
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("total_competitors")]
        public int TotalCompetitors { get; set; }

        [JsonPropertyName("system")]
        [JsonConverter(typeof(PlayerRankingSystemConverter))]
        public PlayerRankingSystem System { get; set; }

        [JsonPropertyName("pvp")]
        public List<PlayerVersusRecord> PlayerVersusPlayerRecords { get; set; }

    }
}
