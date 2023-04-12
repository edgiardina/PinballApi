using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class User
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ifpaId")]
        public int? IfpaId { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }

        [JsonPropertyName("flag")]
        public string Flag { get; set; }

        [JsonPropertyName("location")]
        public string Location { get; set; }

        [JsonPropertyName("pronouns")]
        public string Pronouns { get; set; }

        [JsonPropertyName("initials")]
        public string Initials { get; set; }

        [JsonPropertyName("avatar")]
        public object Avatar { get; set; }

        [JsonPropertyName("banner")]
        public object Banner { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
