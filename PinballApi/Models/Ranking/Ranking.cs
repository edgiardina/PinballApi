using Newtonsoft.Json;

namespace PinballApi.Models.Ranking
{
    public class Ranking
    {
        [JsonProperty("player_id")]
        public string PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("wppr_points")]
        public string WpprPoints { get; set; }

        [JsonProperty("current_wppr_rank")]
        public string CurrentWpprRank { get; set; }

        [JsonProperty("last_month_rank")]
        public string LastMonthRank { get; set; }

        [JsonProperty("rating_value")]
        public string RatingValue { get; set; }

        [JsonProperty("efficiency_percent")]
        public string EfficiencyPercent { get; set; }

        [JsonProperty("event_count")]
        public string EventCount { get; set; }

        [JsonProperty("best_finish")]
        public string BestFinish { get; set; }

        [JsonProperty("best_finish_position")]
        public string BestFinishPosition { get; set; }

        [JsonProperty("best_tournament_id")]
        public string BestTournamentId { get; set; }
    }
}
