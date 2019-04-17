using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class Country
    {
        [JsonProperty("country_name")]
        public string CountryName { get; set; }
    }
}
