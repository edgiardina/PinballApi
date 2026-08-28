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

        /// <summary>
        /// The absolute url of the profile picture. Null when the user has not set one.
        /// </summary>
        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }

        /// <summary>
        /// The absolute url of the profile banner. Null when the user has not set one.
        /// </summary>
        [JsonPropertyName("banner")]
        public string Banner { get; set; }

        /// <summary>
        /// The absolute url of the picture the user shows inside a tournament.
        /// </summary>
        [JsonPropertyName("tournamentAvatar")]
        public string TournamentAvatar { get; set; }

        [JsonPropertyName("createdAt")]
        public DateTime CreatedAt { get; set; }
    }
}
