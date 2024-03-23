using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesWinners
    {
        [JsonPropertyName("series_code")]
        public string SeriesCode { get; set; }

        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("results")]
        public List<SeriesResult> Results { get; set; }
    }
}
