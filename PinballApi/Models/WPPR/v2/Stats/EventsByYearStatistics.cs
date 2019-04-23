using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class EventsByYearStatistics
    {
        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("country_count")]
        public int CountryCount { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("player_count")]
        public int PlayerCount { get; set; }

        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
