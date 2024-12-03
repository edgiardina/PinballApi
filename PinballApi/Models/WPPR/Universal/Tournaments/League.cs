using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PinballApi.Models.WPPR.Universal.Tournaments
{
    public class League
    {
        [JsonPropertyName("tournament_id")]
        public string TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("stateprov")]
        public string Stateprov { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("event_start_date")]
        public DateOnly EventStartDate { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateOnly EventEndDate { get; set; }

        [JsonPropertyName("private_flag")]
        public bool PrivateFlag { get; set; }

        [JsonPropertyName("director_name")]
        public string DirectorName { get; set; }

        [JsonPropertyName("director_id")]
        public int DirectorId { get; set; }
    }
}
