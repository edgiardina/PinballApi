using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class ArenaCount
    {
        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonExtensionData]
        public Dictionary<string, object> Counts { get; set; }
    }
}
