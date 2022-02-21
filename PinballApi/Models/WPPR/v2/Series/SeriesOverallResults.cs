using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesOverallResults
    {
        [JsonProperty("series_code")]
        public string SeriesCode { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }

        [JsonProperty("championship_prize_fund")]
        public double ChampionshipPrizeFund { get; set; }

        [JsonProperty("overall_results")]
        public List<SeriesOverallResult> OverallResults { get; set; }
    }
}
