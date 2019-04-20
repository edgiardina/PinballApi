using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerResult
    {
        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("original_points")]
        public double OriginalPoints { get; set; }

        [JsonProperty("current_points")]
        public double CurrentPoints { get; set; }
    }
}