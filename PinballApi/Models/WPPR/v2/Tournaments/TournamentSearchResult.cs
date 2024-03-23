using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentSearchResult
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("event_type")]
        public string EventType { get; set; }

        [JsonPropertyName("event_start_date")]
        public DateTime EventStartDate { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateTime EventEndDate { get; set; }
    }
}
