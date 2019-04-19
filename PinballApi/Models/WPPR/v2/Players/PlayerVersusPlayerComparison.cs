using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerVersusPlayerComparison
    {
        [JsonProperty("player_1")]
        public Player PlayerOne { get; set; }

        [JsonProperty("player_2")]
        public Player PlayerTwo { get; set; }

        [JsonProperty("pvp")]
        public List<PlayerVersusPlayerComparisonRecord> ComparisonRecords { get; set; }
    }
}
