using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class Country
    {
        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
    }
}