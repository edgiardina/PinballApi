using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Director
{
    public class Director
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("director_id")]
        public long DirectorId { get; set; }

        [JsonPropertyName("facebook_url")]
        public string FacebookUrl { get; set; }

        [JsonPropertyName("twitch_url")]
        public string TwitchUrl { get; set; }

        [JsonPropertyName("profile_photo_url")]
        public string ProfilePhotoUrl { get; set; }

        [JsonPropertyName("stats")]
        public DirectorStats Stats { get; set; }
    }
}
