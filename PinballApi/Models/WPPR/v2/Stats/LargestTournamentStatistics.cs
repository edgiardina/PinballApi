using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class LargestTournamentStatistics
    {
        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("player_count")]
        public int PlayerCount { get; set; }

        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("tournament_date")]
        public DateTime TournamentDate { get; set; }

        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
