using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class PlayerResult
    {
        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_date")]
        public DateOnly EventDate { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("original_points")]
        public double OriginalPoints { get; set; }

        [JsonPropertyName("current_points")]
        public double? CurrentPoints { get; set; }
    }
}