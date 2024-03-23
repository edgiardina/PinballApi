using System.Text.Json.Serialization;
using PinballApi.Converters;
using System;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class PlayerStats
    {
        [JsonPropertyName("current_wppr_rank")]
        public int CurrentWpprRank { get; set; }

        [JsonPropertyName("last_month_rank")]
        public int LastMonthRank { get; set; }

        [JsonPropertyName("last_year_rank")]
        public int LastYearRank { get; set; }

        [JsonPropertyName("highest_rank")]
        public int HighestRank { get; set; }

        [JsonPropertyName("highest_rank_date")]
        public DateTime? HighestRankDate { get; set; }

        [JsonPropertyName("current_wppr_value")]
        public double CurrentWpprValue { get; set; }

        [JsonPropertyName("wppr_points_all_time")]
        public double WpprPointsAllTime { get; set; }

        [JsonPropertyName("best_finish")]
        public int BestFinish { get; set; }

        [JsonPropertyName("best_finish_count")]
        public int BestFinishCount { get; set; }

        [JsonPropertyName("average_finish")]
        public int AverageFinish { get; set; }

        [JsonPropertyName("average_finish_last_year")]
        public int AverageFinishLastYear { get; set; }

        [JsonPropertyName("total_events_all_time")]
        public int TotalEventsAllTime { get; set; }

        [JsonPropertyName("total_active_events")]
        public int TotalActiveEvents { get; set; }

        [JsonPropertyName("total_events_away")]
        public int TotalEventsAway { get; set; }

        //TODO: These four properties fail to load if you view a suppressed player. 
        [JsonPropertyName("ratings_rank")]
        [JsonConverter(typeof(NotRankedNullableDescriptiveConverter))]
        public int? RatingsRank { get; set; }

        [JsonPropertyName("ratings_value")]
        public double? RatingsValue { get; set; }

        [JsonPropertyName("efficiency_rank")]
        [JsonConverter(typeof(NotRankedNullableDescriptiveConverter))]
        public int? EfficiencyRank { get; set; }

        [JsonPropertyName("efficiency_value")]
        [JsonConverter(typeof(EmptyStringNullableDoubleDescriptiveConverter))]
        public double? EfficiencyValue { get; set; }
    }

}
