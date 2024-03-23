using System.Text.Json.Serialization;
using System;
using PinballApi.Converters;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class CalendarDetails
    {
        [JsonPropertyName("calendar_id")]
        public int CalendarId { get; set; }
        
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }
        
        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("address1")]
        public string Address1 { get; set; }

        [JsonPropertyName("address2")]
        public string Address2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; }
        
        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }
        
        [JsonPropertyName("start_date")]
        public DateTime StartDate { get; set; }

        [JsonPropertyName("end_date")]
        public DateTime EndDate { get; set; }

        [JsonPropertyName("director_name")]
        public string DirectorName { get; set; }
        
        [JsonPropertyName("latitude")]
        public double Latitude { get; set; }

        [JsonPropertyName("longitude")]
        public double Longitude { get; set; }

        [JsonPropertyName("details")]
        public string Details { get; set; }
        
        [JsonPropertyName("private_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public string PrivateFlag { get; set; }

        [JsonPropertyName("distance")]
        public int Distance { get; set; }

        [JsonPropertyName("average_players")]
        public string AveragePlayers { get; set; }

        [JsonPropertyName("average_points")]
        public string AveragePoints { get; set; }
    }
}
