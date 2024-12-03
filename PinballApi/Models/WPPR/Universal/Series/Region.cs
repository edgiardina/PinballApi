using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class Region
    {
        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }

        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }
    }
}