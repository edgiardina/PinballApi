using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class BestGameStats
    {
        [JsonPropertyName("arenaData")]
        public List<ArenaStats> ArenaData { get; set; }

        [JsonPropertyName("singlePlayerGameData")]
        public List<SinglePlayerGameData> SinglePlayerGameData { get; set; }
    }
}
