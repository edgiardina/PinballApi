using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class UserCount
    {
        [JsonPropertyName("tournamentOrganizedCount")]
        public int TournamentOrganizedCount { get; set; }

        [JsonPropertyName("seriesOrganizedCount")]
        public int SeriesOrganizedCount { get; set; }

        [JsonPropertyName("tournamentPlayCount")]
        public int TournamentPlayCount { get; set; }

        [JsonPropertyName("ratingPeriodCount")]
        public int RatingPeriodCount { get; set; }
    }
}
