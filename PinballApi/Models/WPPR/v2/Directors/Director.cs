using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Directors
{
    public class Director
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("profile_photo")]
        public Uri ProfilePhoto { get; set; }
    }
}
