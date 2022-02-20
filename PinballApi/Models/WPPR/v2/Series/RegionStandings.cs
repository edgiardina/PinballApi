using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class RegionStandings
    {
        [JsonProperty("series_code")]
        public string SeriesCode { get; set; }

        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("region_name")]
        public string RegionName { get; set; }

        [JsonProperty("prize_fund")]
        public double PrizeFund { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("standings")]
        public List<RegionStanding> Standings { get; set; }
    }
}
