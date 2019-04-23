using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v1.Tournaments
{
    public class TournamentSearchItem
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("event_count")]
        public int EventCount { get; set; }

        [JsonProperty("event_type")]
        public string EventType { get; set; }

        [JsonProperty("last_event_date")]
        public DateTime LastEventDate { get; set; }

    }
}
