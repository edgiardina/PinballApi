using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class ActiveLeague
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("event_start_date")]
        public DateTime EventStartDate { get; set; }

        [JsonProperty("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonProperty("private_flag")]
        public bool PrivateFlag { get; set; }
    }
}
