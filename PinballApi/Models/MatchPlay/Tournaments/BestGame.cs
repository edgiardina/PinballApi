using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class BestGame
    {
        [JsonProperty("arena")]
        public Arena Arenas { get; set; }

        [JsonProperty("voidedGameCount")]
        public int VoidedGameCount { get; set; }

        [JsonProperty("singlePlayerGames")]
        public List<SinglePlayerGame> SinglePlayerGames { get; set; }

        [JsonProperty("queue")]
        public List<Queue> Queues { get; set; }
    }
}
