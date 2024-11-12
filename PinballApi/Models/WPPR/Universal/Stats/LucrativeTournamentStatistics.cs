using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.Universal.Stats
{
    public class LucrativeTournamentStatistics
    {
        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
        
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("tournament_date")]
        public DateOnly TournamentDate { get; set; }

        [JsonPropertyName("tournament_value")]
        public double TournamentValue { get; set; }

        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
