using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentResults
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("ranking_system")]
        public string RankingSystem { get; set; }

        [JsonProperty("results")]
        public List<TournamentResult> Results { get; set; }
    }
}
