using PinballApi.Converters;
using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings
{
    public abstract class BaseRanking
    {
        [JsonPropertyName("player_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long PlayerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("age")]
        [JsonConverter(typeof(EmptyStringNullableIntDescriptiveConverter))]
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
        public long CurrentRank { get; set; }

        [JsonPropertyName("efficiency_percent")]
        public double EfficiencyPercent { get; set; }
    }
}
