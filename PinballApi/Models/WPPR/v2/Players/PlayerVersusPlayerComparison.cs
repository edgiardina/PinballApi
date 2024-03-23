using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerVersusPlayerComparison
    {
        [JsonPropertyName("player_1")]
        public Player PlayerOne { get; set; }

        [JsonPropertyName("player_2")]
        public Player PlayerTwo { get; set; }

        [JsonPropertyName("pvp")]
        public List<PlayerVersusPlayerComparisonRecord> ComparisonRecords { get; set; }
    }
}
