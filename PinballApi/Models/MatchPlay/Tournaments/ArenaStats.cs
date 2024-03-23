using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class ArenaStats
    {
        [JsonPropertyName("arenaId")]
        public int ArenaId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("gameCount")]
        public int GameCount { get; set; }

        [JsonPropertyName("avgDuration")]
        public int AvgDuration { get; set; }
    }
}
