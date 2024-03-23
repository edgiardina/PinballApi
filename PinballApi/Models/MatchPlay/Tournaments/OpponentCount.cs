using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class OpponentCount
    {
        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Counts { get; set; }
    }
}
