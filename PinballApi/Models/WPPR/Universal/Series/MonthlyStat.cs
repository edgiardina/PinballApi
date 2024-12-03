using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class MonthlyStat
    {
        [JsonPropertyName("month")]
        public int Month { get; set; }

        [JsonPropertyName("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonPropertyName("player_count")]
        public int PlayerCount { get; set; }

        [JsonPropertyName("unique_player_count")]
        public int UniquePlayerCount { get; set; }

        [JsonPropertyName("prize_fund")]
        public double PrizeFund { get; set; }
    }
}