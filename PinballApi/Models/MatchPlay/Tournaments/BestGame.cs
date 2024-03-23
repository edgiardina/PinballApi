using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class BestGame
    {
        [JsonPropertyName("arena")]
        public Arena Arenas { get; set; }

        [JsonPropertyName("voidedGameCount")]
        public int VoidedGameCount { get; set; }

        [JsonPropertyName("singlePlayerGames")]
        public List<SinglePlayerGame> SinglePlayerGames { get; set; }

        [JsonPropertyName("queue")]
        public List<Queue> Queues { get; set; }
    }
}
