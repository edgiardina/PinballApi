using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentResult
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("position")]
        public int Position { get; set; }

        [JsonProperty("points")]
        public double Points { get; set; }

        [JsonProperty("wppr_rank")]
        public int WpprRank { get; set; }

        [JsonProperty("ratings_value")]
        public double RatingsValue { get; set; }

        [JsonProperty("excluded_flag")]
        public bool ExcludedFlag { get; set; }

        [JsonProperty("player_tournament_count")]
        public int PlayerTournamentCount { get; set; }
    }
}