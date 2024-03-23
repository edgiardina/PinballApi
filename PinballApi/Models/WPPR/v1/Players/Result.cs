using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class Result
    {
        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("periodic_flag")]
        public string PeriodicFlag { get; set; }

        [JsonPropertyName("wppr_state")]
        public string WpprState { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("original_points")]
        public double OriginalPoints { get; set; }

        [JsonPropertyName("current_points")]
        public double CurrentPoints { get; set; }


    }
}
