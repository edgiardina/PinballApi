using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.Universal.Directors
{
    public class Director
    {
        [JsonPropertyName("player_profile")]
        public PlayerProfile PlayerProfile { get; set; }

        [JsonPropertyName("director_profile")]
        public DirectorProfile? DirectorProfile { get; set; }
    }
}
