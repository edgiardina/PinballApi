using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class TournamentsByStateStatistics
    {
        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("total_points_all")]
        public double TotalPointsAll { get; set; }

        [JsonPropertyName("total_points_tournament_value")]
        public double TotalPointsTourValue { get; set; }

        [JsonPropertyName("stats_rank")]
        public int StatsRank { get; set; }
    }
}
