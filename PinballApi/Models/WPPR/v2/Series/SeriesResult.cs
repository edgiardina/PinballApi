using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesResult
    {
        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }

        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("winners")]
        public List<SeriesWinner> Winners { get; set; }

    }
}
