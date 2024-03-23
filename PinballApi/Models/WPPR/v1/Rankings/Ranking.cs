using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v1.Rankings
{
    public class Ranking
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonPropertyName("current_wppr_rank")]
        public int CurrentWpprRank { get; set; }

        [JsonPropertyName("last_month_rank")]
        public int LastMonthRank { get; set; }

        [JsonPropertyName("rating_value")]
        public double RatingValue { get; set; }

        [JsonPropertyName("efficiency_percent")]
        public double? EfficiencyPercent { get; set; }

        [JsonPropertyName("event_count")]
        public int EventCount { get; set; }

        [JsonPropertyName("best_finish")]
        public string BestFinish { get; set; }

        [JsonPropertyName("best_finish_position")]
        public int BestFinishPosition { get; set; }

        [JsonPropertyName("best_tournament_id")]
        public int BestTournamentId { get; set; }
    }
}
