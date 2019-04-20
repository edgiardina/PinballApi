using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class EliteRankingResult
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("age")]
        public int? Age { get; set; }

        [JsonProperty("profile_photo", NullValueHandling = NullValueHandling.Ignore)]
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

        [JsonProperty("wppr_rank")]
        public int WpprRank { get; set; }

        [JsonProperty("elite_rank")]
        public int EliteRank { get; set; }

        [JsonProperty("win_percentage")]
        public double WinPercentage { get; set; }

        [JsonProperty("total_wins")]
        public int TotalWins { get; set; }

        [JsonProperty("total_loses")]
        public int TotalLosses { get; set; }

        [JsonProperty("total_draws")]
        public int TotalDraws { get; set; }

        [JsonProperty("total_player_count")]
        public int TotalPlayerCount { get; set; }
    }
}