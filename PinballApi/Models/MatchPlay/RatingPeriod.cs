using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class RatingPeriod
    {
        [JsonPropertyName("ratingPeriodId")]
        public int RatingPeriodId { get; set; }

        [JsonPropertyName("periodDate")]
        public DateTime PeriodDate { get; set; }

        [JsonPropertyName("tournamentCount")]
        public int? TournamentCount { get; set; }

        [JsonPropertyName("challengeCount")]
        public int? ChallengeCount { get; set; }

        [JsonPropertyName("extEventCount")]
        public int? ExtEventCount { get; set; }

        [JsonPropertyName("resultCount")]
        public int? ResultCount { get; set; }

        [JsonPropertyName("playerCount")]
        public int? PlayerCount { get; set; }
    }
}
