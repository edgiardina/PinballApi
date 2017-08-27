using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.Calendar
{
    public class Country
    {
        [JsonProperty("country_name")]
        public string CountryName { get; set; }
    }
}
