using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.Universal.Stats
{
    public class EventsByYearStatistics
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("country_count")]
        public int CountryCount { get; set; }

        [JsonPropertyName("player_count")]
        public int PlayerCount { get; set; }

        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
