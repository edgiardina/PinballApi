using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Tournaments
{
    public class TournamentResult
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }

        [JsonProperty("periodic_count")]
        public int PeriodicCount { get; set; }

        [JsonProperty("rating_strength")]
        public double? RatingStrength { get; set; }

        [JsonProperty("ranking_strength")]
        public double? RankingStrength { get; set; }

        [JsonProperty("base_value")]
        public double BaseValue { get; set; }

        [JsonProperty("event_value")]
        public double EventValue { get; set; }

        [JsonProperty("results")]
        public List<Result> Results { get; set; }
    }
}
