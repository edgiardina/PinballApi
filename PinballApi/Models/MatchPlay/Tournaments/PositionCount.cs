using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class PositionCount
    {
        [JsonPropertyName("1")]
        public int _1 { get; set; }

        [JsonPropertyName("0")]
        public int _0 { get; set; }

        [JsonPropertyName("3")]
        public int _3 { get; set; }

        [JsonPropertyName("2")]
        public int _2 { get; set; }

        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }
    }
}
