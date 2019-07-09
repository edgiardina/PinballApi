using Newtonsoft.Json;
using PinballApi.Converters;
using System;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class PlayerStats
    {
        [JsonProperty("current_wppr_rank")]
        public int CurrentWpprRank { get; set; }

        [JsonProperty("last_month_rank")]
        public int LastMonthRank { get; set; }

        [JsonProperty("last_year_rank")]
        public int LastYearRank { get; set; }

        [JsonProperty("highest_rank")]
        public int HighestRank { get; set; }

        [JsonProperty("highest_rank_date")]
        [JsonConverter(typeof(NullableDateConverter))]
        public DateTime? HighestRankDate { get; set; }

        [JsonProperty("current_wppr_points")]
        public double CurrentWpprValue { get; set; }

        [JsonProperty("all_time_wppr_points")]
        public double WpprPointsAllTime { get; set; }

        [JsonProperty("best_finish")]
        public int BestFinish { get; set; }

        [JsonProperty("best_finish_count")]
        public int BestFinishCount { get; set; }

        [JsonProperty("average_finish")]
        public int AverageFinish { get; set; }

        [JsonProperty("average_finish_last_year")]
        public int AverageFinishLastYear { get; set; }

        [JsonProperty("total_events_all_time")]
        public int TotalEventsAllTime { get; set; }

        [JsonProperty("total_active_events")]
        public int TotalActiveEvents { get; set; }

        [JsonProperty("total_events_away")]
        public int TotalEventsAway { get; set; }

        //TODO: These four properties fail to load if you view a suppressed player. 
        [JsonProperty("ratings_rank")]
        [JsonConverter(typeof(IntegerWithNullDescriptiveConverter), "Not Ranked")]
        public int? RatingsRank { get; set; }

        [JsonProperty("ratings_value")]
        public double? RatingsValue { get; set; }

        [JsonProperty("efficiency_rank")]
        [JsonConverter(typeof(IntegerWithNullDescriptiveConverter), "Not Ranked")]
        public int? EfficiencyRank { get; set; }

        [JsonProperty("efficiency_value")]
        public double? EfficiencyValue { get; set; }
    }

}
