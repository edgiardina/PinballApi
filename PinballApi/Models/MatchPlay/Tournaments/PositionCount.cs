using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class PositionCount
    {
        [JsonProperty("1")]
        public int _1 { get; set; }

        [JsonProperty("0")]
        public int _0 { get; set; }

        [JsonProperty("3")]
        public int _3 { get; set; }

        [JsonProperty("2")]
        public int _2 { get; set; }

        [JsonProperty("playerId")]
        public int PlayerId { get; set; }
    }
}
