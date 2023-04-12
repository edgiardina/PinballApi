using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class PlayerStats
    {
        [JsonProperty("positionCounts")]
        public List<PositionCount> PositionCounts { get; set; }

        [JsonProperty("arenaCounts")]
        public List<ArenaCount> ArenaCounts { get; set; }

        [JsonProperty("opponentCounts")]
        public List<OpponentCount> OpponentCounts { get; set; }
    }
}
