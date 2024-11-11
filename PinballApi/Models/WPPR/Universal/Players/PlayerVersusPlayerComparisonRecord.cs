using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class PlayerVersusPlayerComparisonRecord
    {
        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("tournament_id")]
        public string TournamentId { get; set; }

        [JsonPropertyName("event_end_dt")]
        public DateOnly EventDate { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("finish_position")]
        public FinishPosition FinishPosition { get; set; }
    }
}
