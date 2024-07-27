using PinballApi.Converters;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class MatchplayEvents
    {
        [JsonPropertyName("id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long Id { get; set; }

        [JsonPropertyName("rating")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long Rating { get; set; }

        [JsonPropertyName("rank")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long Rank { get; set; }
    }
}
