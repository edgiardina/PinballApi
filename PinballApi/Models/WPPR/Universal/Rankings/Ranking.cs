using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Rankings
{
    public class Ranking : BaseRanking
    {
        [JsonPropertyName("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonPropertyName("total_wins_last_3_years")]
        public int TotalWinsLast3Years { get; set; }

        [JsonPropertyName("top_3_last_3_years")]
        public int Top3Last3Years { get; set; }

        [JsonPropertyName("top_10_last_3_years")]
        public int Top10Last3Years { get; set; }

        [JsonPropertyName("last_month_rank")]
        public long LastMonthRank { get; set; }

        [JsonPropertyName("rating_value")]
        public double RatingValue { get; set; }

        [JsonPropertyName("rating_deviation")]
        public double RatingDeviation { get; set; }

        [JsonPropertyName("event_count")]
        public int EventCount { get; set; }

        [JsonPropertyName("best_finish")]
        public string BestFinish { get; set; }

        [JsonPropertyName("best_finish_position")]
        public int BestFinishPosition { get; set; }

        [JsonPropertyName("best_tournament_id")]
        public long BestTournamentId { get; set; }
    }
}
