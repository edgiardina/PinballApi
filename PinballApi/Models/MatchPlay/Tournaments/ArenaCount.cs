using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class ArenaCount
    {
        [JsonProperty("playerId")]
        public int PlayerId { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Counts { get; set; }
    }
}
