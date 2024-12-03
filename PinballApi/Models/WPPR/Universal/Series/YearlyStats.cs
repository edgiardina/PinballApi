using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class YearlyStats
    {
        [JsonPropertyName("player_count")]
        public int PlayerCount { get; set; }

        [JsonPropertyName("test_count")]
        public int TestCount { get; set; }

        [JsonPropertyName("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonPropertyName("prize_fund")]
        public double PrizeFund { get; set; }

        [JsonPropertyName("field_size")]
        public int FieldSize { get; set; }
    }
}