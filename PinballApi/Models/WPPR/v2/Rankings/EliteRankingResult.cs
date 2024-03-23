using System.Text.Json.Serialization;
using System;
using PinballApi.Converters;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class EliteRankingResult
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("age")]
        [JsonConverter(typeof(EmptyStringNullableIntDescriptiveConverter))]
        public int? Age { get; set; }

        [JsonPropertyName("profile_photo")]
        public Uri ProfilePhoto { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonPropertyName("wppr_rank")]
        public int WpprRank { get; set; }

        [JsonPropertyName("elite_rank")]
        public int EliteRank { get; set; }

        [JsonPropertyName("win_percentage")]
        public double WinPercentage { get; set; }

        [JsonPropertyName("total_wins")]
        public int TotalWins { get; set; }

        [JsonPropertyName("total_loses")]
        public int TotalLosses { get; set; }

        [JsonPropertyName("total_draws")]
        public int TotalDraws { get; set; }

        [JsonPropertyName("total_player_count")]
        public int TotalPlayerCount { get; set; }
    }
}