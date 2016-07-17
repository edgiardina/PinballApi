using Newtonsoft.Json;

namespace PinballApi.Models.Ranking
{
    public class PlayerStats
    {
        [JsonProperty("current_wppr_rank")]
        public string CurrentWpprRank { get; set; }

        [JsonProperty("last_month_rank")]
        public string LastMonthRank { get; set; }

        [JsonProperty("last_year_rank")]
        public string LastYearRank { get; set; }

        [JsonProperty("highest_rank")]
        public string HighestRank { get; set; }

        [JsonProperty("highest_rank_date")]
        public string HighestRankDate { get; set; }

        [JsonProperty("current_wppr_value")]
        public string CurrentWpprValue { get; set; }

        [JsonProperty("wppr_points_all_time")]
        public string WpprPointsAllTime { get; set; }

        [JsonProperty("best_finish")]
        public string BestFinish { get; set; }

        [JsonProperty("best_finish_count")]
        public string BestFinishCount { get; set; }

        [JsonProperty("average_finish")]
        public string AverageFinish { get; set; }

        [JsonProperty("average_finish_last_year")]
        public string AverageFinishLastYear { get; set; }

        [JsonProperty("total_events_all_time")]
        public string TotalEventsAllTime { get; set; }

        [JsonProperty("total_active_events")]
        public string TotalActiveEvents { get; set; }

        [JsonProperty("total_events_away")]
        public string TotalEventsAway { get; set; }

        [JsonProperty("ratings_rank")]
        public string RatingsRank { get; set; }

        [JsonProperty("ratings_value")]
        public string RatingsValue { get; set; }

        [JsonProperty("efficiency_rank")]
        public string EfficiencyRank { get; set; }

        [JsonProperty("efficiency_value")]
        public double EfficiencyValue { get; set; }
    }

}
