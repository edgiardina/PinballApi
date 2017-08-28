using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.Calendar
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
        public DateTime start_date { get; set; }

        [JsonProperty("end_date")]
        public DateTime end_date { get; set; }

        [JsonProperty("director_name")]
        public string DirectorName { get; set; }
        
        [JsonProperty("latitude")]
        public double latitude { get; set; }

        [JsonProperty("longitude")]
        public double longitude { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; }
        
        [JsonProperty("private_flag")]
        public string PrivateFlag { get; set; }
    }
}
