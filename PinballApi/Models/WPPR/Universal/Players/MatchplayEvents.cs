using PinballApi.Converters;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class MatchplayEvents
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(NullableLongIntegerFromStringConverter))]
        public long? Id { get; set; }

        [JsonPropertyName("rating")]
        [JsonConverter(typeof(NullableLongIntegerFromStringConverter))]
        public long? Rating { get; set; }

        [JsonPropertyName("rank")]
        [JsonConverter(typeof(NullableLongIntegerFromStringConverter))]
        public long? Rank { get; set; }
    }
}
