using System.Text.Json.Serialization;
using PinballApi.Converters;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentResult
    {
        /// <summary>
        /// Is null if player is suppressed.
        /// </summary> 
        [JsonPropertyName("player_id")]
        [JsonConverter(typeof(EmptyStringNullableIntDescriptiveConverter))]
        public int? PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("position")]
        public int Position { get; set; }

        [JsonPropertyName("points")]
        public double Points { get; set; }

        [JsonPropertyName("wppr_rank")]
        public int WpprRank { get; set; }

        [JsonPropertyName("ratings_value")]
        [JsonConverter(typeof(NotRatedNullableDescriptiveConverter))]
        public double? RatingsValue { get; set; }

        [JsonPropertyName("excluded_flag")]
        [JsonConverter(typeof(BooleanConverter))]
        public bool ExcludedFlag { get; set; }

        [JsonPropertyName("player_tournament_count")]
        public int PlayerTournamentCount { get; set; }
    }
}