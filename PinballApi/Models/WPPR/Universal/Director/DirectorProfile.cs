using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Directors
{
    public class DirectorProfile
    {
        [JsonPropertyName("director_id")]
        public int DirectorId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("profile_photo")]
        public Uri ProfilePhoto { get; set; }
    }
}