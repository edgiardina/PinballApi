using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class SeriesStats
    {
        [JsonPropertyName("series_code")]
        public string SeriesCode { get; set; }

        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("monthly_stats")]
        public List<MonthlyStat> MonthlyStats { get; set; }

        [JsonPropertyName("yearly_stats")]
        public YearlyStats YearlyStats { get; set; }

        [JsonPropertyName("payouts")]
        public List<Payout> Payouts { get; set; }
    }
}