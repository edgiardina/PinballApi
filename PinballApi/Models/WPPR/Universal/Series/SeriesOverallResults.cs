using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class SeriesOverallResults
    {
        [JsonPropertyName("series_code")]
        public string SeriesCode { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("championship_prize_fund")]
        public double ChampionshipPrizeFund { get; set; }

        [JsonPropertyName("overall_results")]
        public List<SeriesOverallResult> OverallResults { get; set; }
    }
}
