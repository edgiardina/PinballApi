using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class BestGameStats
    {
        [JsonPropertyName("gameCount")]
        public int GameCount { get; set; }

        [JsonPropertyName("voidedCount")]
        public int VoidedCount { get; set; }
    }
}
