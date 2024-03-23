using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class PlayerStats
    {
        [JsonPropertyName("positionCounts")]
        public List<PositionCount> PositionCounts { get; set; }

        [JsonPropertyName("arenaCounts")]
        public List<ArenaCount> ArenaCounts { get; set; }

        [JsonPropertyName("opponentCounts")]
        public List<OpponentCount> OpponentCounts { get; set; }
    }
}
