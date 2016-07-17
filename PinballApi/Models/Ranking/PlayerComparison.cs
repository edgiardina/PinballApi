using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.Ranking
{
    public class PlayerComparison
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("total_competitors")]
        public int TotalCompetitors { get; set; }

        [JsonProperty("pvp")]
        public IList<Pvp> Pvp { get; set; }
    }
}
