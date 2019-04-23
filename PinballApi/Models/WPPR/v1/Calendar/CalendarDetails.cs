using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class CalendarDetails
    {
        [JsonProperty("calendar_id")]
        public int CalendarId { get; set; }
        
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }
        
        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("address1")]
        public string Address1 { get; set; }

        [JsonProperty("address2")]
        public string Address2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("zipcode")]
        public string Zipcode { get; set; }
        
        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }
        
        [JsonProperty("start_date")]
        public DateTime StartDate { get; set; }

        [JsonProperty("end_date")]
        public DateTime EndDate { get; set; }

        [JsonProperty("director_name")]
        public string DirectorName { get; set; }
        
        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }
        
        [JsonProperty("private_flag")]
        public string PrivateFlag { get; set; }

        [JsonProperty("distance")]
        public int Distance { get; set; }

        [JsonProperty("average_players")]
        public string AveragePlayers { get; set; }

        [JsonProperty("average_points")]
        public string AveragePoints { get; set; }
    }
}
