using PinballApi.Converters;
using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class PlayerSystem
    {
        [JsonPropertyName("system")]
        [JsonConverter(typeof(PlayerRankingSystemConverter))]
        public PlayerRankingSystem System { get; set; }

        [JsonPropertyName("current_rank")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long CurrentRank { get; set; }

        [JsonPropertyName("last_month_rank")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long LastMonthRank { get; set; }

        [JsonPropertyName("last_year_rank")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long? LastYearRank { get; set; }

        [JsonPropertyName("highest_rank")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long HighestRank { get; set; }

        [JsonPropertyName("highest_rank_date")]
        [JsonConverter(typeof(NullableDateConverter))]
        public DateTimeOffset? HighestRankDate { get; set; }

        [JsonPropertyName("pro_rank")]
        [JsonConverter(typeof(EmptyStringNullableIntDescriptiveConverter))]
        public int? ProRank { get; set; }

        [JsonPropertyName("current_points")]
        public double? CurrentPoints { get; set; }

        [JsonPropertyName("all_time_points")]
        public double AllTimePoints { get; set; }

        [JsonPropertyName("active_points")]
        public double? ActivePoints { get; set; }

        [JsonPropertyName("inactive_points")]
        public double? InactivePoints { get; set; }

        [JsonPropertyName("best_finish")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long BestFinish { get; set; }

        [JsonPropertyName("best_finish_count")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long BestFinishCount { get; set; }

        [JsonPropertyName("average_finish")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long AverageFinish { get; set; }

        [JsonPropertyName("average_finish_last_year")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long? AverageFinishLastYear { get; set; }

        [JsonPropertyName("total_events_all_time")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long TotalEventsAllTime { get; set; }

        [JsonPropertyName("total_active_events")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long TotalActiveEvents { get; set; }

        [JsonPropertyName("total_events_away")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long TotalEventsAway { get; set; }

        [JsonPropertyName("total_wins_last_3_years")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long TotalWinsLast3_Years { get; set; }

        [JsonPropertyName("top_3_last_3_years")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long Top3_Last3_Years { get; set; }

        [JsonPropertyName("top_10_last_3_years")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long Top10_Last3_Years { get; set; }

        [JsonPropertyName("ratings_rank")]
        [JsonConverter(typeof(NullableLongIntegerFromStringConverter))]
        public long? RatingsRank { get; set; }

        [JsonPropertyName("ratings_value")]
        [JsonConverter(typeof(EmptyStringNullableDoubleDescriptiveConverter))]
        public double? RatingsValue { get; set; }

        [JsonPropertyName("efficiency_rank")]
        [JsonConverter(typeof(NullableLongIntegerFromStringConverter))]
        public long? EfficiencyRank { get; set; }

        [JsonPropertyName("efficiency_value")]
        [JsonConverter(typeof(EmptyStringNullableDoubleDescriptiveConverter))]
        public double? EfficiencyValue { get; set; }
    }
}
