using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class RegionRepresentative
    {
        [JsonPropertyName("player_key")]
        public int PlayerKey { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }

        [JsonPropertyName("profile_photo")]
        public Uri ProfilePhoto { get; set; }
    }
}