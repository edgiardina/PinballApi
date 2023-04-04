using Newtonsoft.Json;
using PinballApi.Models.MatchPlay.Tournaments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class Arena
    {
        [JsonPropertyName("arenaId")]
        public int ArenaId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("opdbId")]
        public string OpdbId { get; set; }

        [JsonPropertyName("categoryId")]
        public int? CategoryId { get; set; }

        [JsonPropertyName("organizerId")]
        public int OrganizerId { get; set; }

        [JsonProperty("tournamentArena")]
        public TournamentArena TournamentArena { get; set; }
    }
}
