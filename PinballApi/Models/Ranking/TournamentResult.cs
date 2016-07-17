using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.Ranking
{
    public class TournamentResults
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("results_count")]
        public int ResultsCount { get; set; }

        [JsonProperty("results")]
        public IList<Result> Results { get; set; }
    }
}
