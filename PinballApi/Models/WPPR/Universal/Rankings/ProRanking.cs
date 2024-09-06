using PinballApi.Converters;
using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings
{
    public class ProRanking
    {
        [JsonPropertyName("player_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long PlayerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("age")]
        public int? Age { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        [JsonPropertyName("profile_photo")]
        public Uri ProfilePhoto { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("stateprov")]
        public string Stateprov { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("current_rank")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long CurrentRank { get; set; }

        [JsonPropertyName("pro_points")]
        public double ProPoints { get; set; }

        [JsonPropertyName("orginal_wppr_points")]
        public double OrginalWpprPoints { get; set; }

        [JsonPropertyName("efficiency_percent")]
        public double EfficiencyPercent { get; set; }

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
