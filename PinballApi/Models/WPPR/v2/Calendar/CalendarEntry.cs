using System.Text.Json.Serialization;
using System;
using PinballApi.Converters;
using PinballApi.Models.WPPR.v2.Tournaments;

namespace PinballApi.Models.WPPR.v2.Calendar
{
    public class CalendarEntry
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("address1")]
        public string Address1 { get; set; }

        [JsonPropertyName("address2")]
        public string Address2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("postal_code")]
        public string PostalCode { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("latitude")]
        public string Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public string Longitude { get; set; }

        [JsonPropertyName("website")]
        public Uri Website { get; set; }

        [JsonPropertyName("start_date")]
        public DateTimeOffset StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTimeOffset EndDate { get; set; }

        [JsonPropertyName("director_name")]
        public string DirectorName { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }

        [JsonPropertyName("private_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool PrivateFlag { get; set; }

        [JsonPropertyName("ranking_system")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public RankingType RankingSystem { get; set; }

        [JsonPropertyName("distance")]
        public long Distance { get; set; }
    }
}
