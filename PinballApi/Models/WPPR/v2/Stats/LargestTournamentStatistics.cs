using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class LargestTournamentStatistics
    {
        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("player_count")]
        public int PlayerCount { get; set; }

        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("tournament_date")]
        public DateTime TournamentDate { get; set; }

        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
