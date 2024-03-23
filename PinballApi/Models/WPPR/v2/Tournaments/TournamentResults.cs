using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Tournaments
{
    public class TournamentResults
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("ranking_system")]
        public string RankingSystem { get; set; }

        [JsonPropertyName("results")]
        public List<TournamentResult> Results { get; set; }
    }
}
