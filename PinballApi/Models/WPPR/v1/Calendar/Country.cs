using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v1.Calendar
{
    public class Country
    {
        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }
    }
}
