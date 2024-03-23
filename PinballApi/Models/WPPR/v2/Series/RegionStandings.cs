using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class RegionStandings
    {
        [JsonPropertyName("series_code")]
        public string SeriesCode { get; set; }

        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }

        [JsonPropertyName("prize_fund")]
        public double PrizeFund { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("standings")]
        public List<RegionStanding> Standings { get; set; }
    }
}
