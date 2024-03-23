using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentEvent
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_start_date")]
        public DateTime EventStartDate { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("year_count")]
        public int YearCount { get; set; }
    }
}