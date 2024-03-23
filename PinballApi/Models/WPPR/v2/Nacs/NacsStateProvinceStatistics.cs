using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsStateProvinceStatistics
    {
        [JsonPropertyName("month")]
        public int Month { get; set; }

        [JsonPropertyName("total_player_count")]
        public int TotalPlayerCount { get; set; }

        [JsonPropertyName("unique_player_count")]
        public int UniquePlayerCount { get; set; }

        [JsonPropertyName("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonPropertyName("prize_accumlation_value")]
        public double PrizeAccumlationValue { get; set; }
    }
}
