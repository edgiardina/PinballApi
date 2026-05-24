using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Other
{
    public class StateProvCountry
    {
        [JsonPropertyName("country_id")]
        public string CountryId { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("regions")]
        public List<StateProvRegion> Regions { get; set; }
    }
}
