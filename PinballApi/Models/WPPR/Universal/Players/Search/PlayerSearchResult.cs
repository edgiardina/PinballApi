using PinballApi.Converters;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players.Search
{
    public class PlayerSearchResult : PlayerBase
    {
        [JsonPropertyName("wppr_rank")]
        [JsonConverter(typeof(NullableLongIntegerFromStringConverter))]
        public long? WpprRank { get; set; }

        [JsonPropertyName("rating_value")]
        [JsonConverter(typeof(EmptyStringNullableDoubleDescriptiveConverter))]
        public double? RatingValue { get; set; }
    }
}