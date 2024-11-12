using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Stats
{
    public class TournamentsByStateStatistics
    {
        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonPropertyName("total_points_all")]
        public double TotalPointsAll { get; set; }

        [JsonPropertyName("total_points_tournament_value")]
        public double TotalPointsTournamentValue { get; set; }

        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
