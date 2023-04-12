using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class ArenaStats
    {
        [JsonProperty("arenaId")]
        public int ArenaId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gameCount")]
        public int GameCount { get; set; }

        [JsonProperty("avgDuration")]
        public int AvgDuration { get; set; }
    }
}
