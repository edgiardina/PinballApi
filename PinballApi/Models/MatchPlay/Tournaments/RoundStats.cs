using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using PinballApi.Converters;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class RoundStats
    {
        [JsonPropertyName("roundId")]
        public int RoundId { get; set; }

        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("createdAt")]
        [JsonConverter(typeof(NonISODateTimeConverter))]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("completedAt")]
        [JsonConverter(typeof(NonISODateTimeConverter))]
        public DateTime CompletedAt { get; set; }

        [JsonPropertyName("gameCount")]
        public int GameCount { get; set; }
    }
}
