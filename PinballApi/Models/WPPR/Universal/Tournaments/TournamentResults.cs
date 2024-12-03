using PinballApi.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Tournaments
{
    public class TournamentResults
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("ranking_system")]
        [JsonConverter(typeof(RankingSystemConverter))]
        public TournamentType RankingSystem { get; set; }

        [JsonPropertyName("results")]
        public List<TournamentResult> Results { get; set; }
    }
}
