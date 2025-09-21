using PinballApi.Converters;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players.Search
{
    public class PlayerSearchResult : PlayerBase
    {
        [JsonPropertyName("wppr_rank")]
        public string WpprRank { get; set; }

        [JsonPropertyName("rating_value")]
        public string RatingValue { get; set; }
    }
}