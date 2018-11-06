using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.Players
{
    public class Result
    {
        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("periodic_flag")]
        public string PeriodicFlag { get; set; }

        [JsonProperty("wppr_state")]
        public string WpprState { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_date")]
        public DateTime EventDate { get; set; }

        [JsonProperty("position")]
        public string Position { get; set; }

        [JsonProperty("original_points")]
        public double OriginalPoints { get; set; }

        [JsonProperty("current_points")]
        public double CurrentPoints { get; set; }


    }
}
