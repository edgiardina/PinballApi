using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class BestGameSummary
    {
        [JsonProperty("arenaId")]
        public int ArenaId { get; set; }

        [JsonProperty("gameCount")]
        public int GameCount { get; set; }

        [JsonProperty("queueCount")]
        public int QueueCount { get; set; }

        [JsonProperty("bestScore")]
        public ulong? BestScore { get; set; }
    }
}
