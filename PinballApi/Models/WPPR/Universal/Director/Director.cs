using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Director
{
    public class Director
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("player_id")]
        public long PlayerId { get; set; }

        [JsonPropertyName("director_id")]
        public long DirectorId { get; set; }

        [JsonPropertyName("twitch_username")]
        public string TwitchUsername { get; set; }

        [JsonPropertyName("profile_photo")]
        public string ProfilePhotoUrl { get; set; }

        [JsonPropertyName("stats")]
        public DirectorStats Stats { get; set; }
    }
}
