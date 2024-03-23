using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class BestGameSummary
    {
        [JsonPropertyName("arenaId")]
        public int ArenaId { get; set; }

        [JsonPropertyName("gameCount")]
        public int GameCount { get; set; }

        [JsonPropertyName("queueCount")]
        public int QueueCount { get; set; }

        [JsonPropertyName("bestScore")]
        public ulong? BestScore { get; set; }
    }
}
