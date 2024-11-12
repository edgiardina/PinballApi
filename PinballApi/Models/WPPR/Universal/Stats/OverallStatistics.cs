using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Stats
{
    public class OverallStatistics
    {
        [JsonPropertyName("overall_player_count")]
        public int OverallPlayerCount { get; set; }

        [JsonPropertyName("active_player_count")]
        public int ActivePlayerCount { get; set; }

        [JsonPropertyName("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonPropertyName("tournament_count_last_month")]
        public int TournamentCountLastMonth { get; set; }

        [JsonPropertyName("tournament_count_this_year")]
        public int TournamentCountThisYear { get; set; }

        [JsonPropertyName("tournament_player_count")]
        public int TournamentPlayerCount { get; set; }

        [JsonPropertyName("tournament_player_count_average")]
        public double TournamentPlayerCountAverage { get; set; }
    }
}
