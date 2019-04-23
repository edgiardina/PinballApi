using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class TournamentsByStateStatistics
    {
        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("total_points_all")]
        public double TotalPointsAll { get; set; }

        [JsonProperty("total_points_tour_value")]
        public double TotalPointsTourValue { get; set; }

        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
