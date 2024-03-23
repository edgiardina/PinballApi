using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class Country
    {
        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
    }
}