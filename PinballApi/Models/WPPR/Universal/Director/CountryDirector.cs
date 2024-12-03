using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Directors
{
    public class CountryDirector
    {
        [JsonPropertyName("player_profile")]
        public PlayerProfile PlayerProfile { get; set; }

        [JsonPropertyName("director_profile")]
        public DirectorProfile? DirectorProfile { get; set; }
    }
}
