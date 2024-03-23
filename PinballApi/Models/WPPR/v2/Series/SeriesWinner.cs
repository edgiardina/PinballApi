using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesWinner
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("profile_photo")]
        public string ProfilePhoto { get; set; }
    }
}
