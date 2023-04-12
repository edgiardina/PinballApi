using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class Challenge
    {
        [JsonPropertyName("challengeId")]
        public int ChallengeId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }
    }
}
