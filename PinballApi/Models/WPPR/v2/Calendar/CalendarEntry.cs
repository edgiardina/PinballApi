using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Calendar
{
    public class CalendarEntry
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("postal_code")]
        public string PostalCode { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [JsonProperty("website")]
        public Uri Website { get; set; }

        [JsonProperty("start_date")]
        public DateTimeOffset StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTimeOffset EndDate { get; set; }

        [JsonProperty("director_name")]
        public string DirectorName { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }

        [JsonProperty("private_flag")]
        public bool PrivateFlag { get; set; }
    }
}
