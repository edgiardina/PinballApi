using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings
{
    public class Ranking : BaseRanking
    {
        [JsonPropertyName("wppr_points")]
        public string WpprPoints { get; set; }

        [JsonPropertyName("total_wins_last_3_years")]
        public string TotalWinsLast3Years { get; set; }

        [JsonPropertyName("top_3_last_3_years")]
        public string Top3Last3Years { get; set; }

        [JsonPropertyName("top_10_last_3_years")]
        public string Top10Last3Years { get; set; }

        [JsonPropertyName("last_month_rank")]
        public string LastMonthRank { get; set; }

        [JsonPropertyName("rating_value")]
        public string RatingValue { get; set; }

        [JsonPropertyName("rating_deviation")]
        public string RatingDeviation { get; set; }

        [JsonPropertyName("event_count")]
        public string EventCount { get; set; }

        [JsonPropertyName("best_finish")]
        public string BestFinish { get; set; }

        [JsonPropertyName("best_finish_position")]
        public string BestFinishPosition { get; set; }

        [JsonPropertyName("best_tournament_id")]
        public string BestTournamentId { get; set; }
    }
}
