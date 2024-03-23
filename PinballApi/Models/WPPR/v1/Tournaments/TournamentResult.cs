using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Tournaments
{
    public class TournamentResult
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; set; }

        [JsonPropertyName("periodic_count")]
        public int PeriodicCount { get; set; }

        [JsonPropertyName("rating_strength")]
        public double? RatingStrength { get; set; }

        [JsonPropertyName("ranking_strength")]
        public double? RankingStrength { get; set; }

        [JsonPropertyName("base_value")]
        public double BaseValue { get; set; }

        [JsonPropertyName("event_value")]
        public double EventValue { get; set; }

        [JsonPropertyName("results")]
        public List<Result> Results { get; set; }
    }
}
