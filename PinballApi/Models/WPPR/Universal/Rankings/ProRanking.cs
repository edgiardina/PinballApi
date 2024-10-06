using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings
{
    public class ProRanking : BaseRanking
    {
        [JsonPropertyName("pro_points")]
        public double ProPoints { get; set; }

        [JsonPropertyName("orginal_wppr_points")]
        public double OriginalWpprPoints { get; set; }

        [JsonPropertyName("adj_efficiency_percent")]
        public double AdjEfficiencyPercent { get; set; }

        [JsonPropertyName("excess_percent")]
        public double ExcessPercent { get; set; }

        [JsonPropertyName("wpprtunity")]
        public double Wpprtunity { get; set; }

        [JsonPropertyName("wppr_adjustment")]
        public double WpprAdjustment { get; set; }

        [JsonPropertyName("sos_percent")]
        public double SosPercent { get; set; }
    }
}
