using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class RankingResult
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("profile_photo")]
        public Uri ProfilePhoto { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonProperty("current_wppr_rank")]
        public int CurrentWpprRank { get; set; }

        [JsonProperty("country_rank")]
        public int CountryRank { get; set; }

        [JsonProperty("last_month_rank")]
        public int LastMonthRank { get; set; }

        [JsonProperty("rating_value")]
        public double RatingValue { get; set; }

        [JsonProperty("rating_deviation")]
        public int RatingDeviation { get; set; }

        [JsonProperty("efficiency_percent")]
        public double EfficiencyPercent { get; set; }

        [JsonProperty("event_count")]
        public int EventCount { get; set; }

        [JsonProperty("best_finish")]
        public string BestFinish { get; set; }

        [JsonProperty("best_finish_position")]
        public int BestFinishPosition { get; set; }

        [JsonProperty("best_tournament_id")]
        public int BestTournamentId { get; set; }
    }
}