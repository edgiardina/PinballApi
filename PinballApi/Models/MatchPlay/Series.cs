using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{

    public class Series
    {
        [JsonPropertyName("seriesId")]
        public int SeriesId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public SeriesStatus Status { get; set; }

        [JsonPropertyName("organizerId")]
        public int OrganizerId { get; set; }

        [JsonPropertyName("test")]
        public bool Test { get; set; }

        [JsonPropertyName("removedResults")]
        public int RemovedResults { get; set; }

        [JsonPropertyName("scoring")]
        public string Scoring { get; set; }

        [JsonPropertyName("estimatedTgp")]
        public object EstimatedTgp { get; set; }

        [JsonPropertyName("tournamentIds")]
        public List<int> TournamentIds { get; } = new List<int>();
    }
}
