using Newtonsoft.Json;
using PinballApi.Models.MatchPlay.Tournaments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay
{
    public class Player
    {
        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("ifpaId")]
        public int? IfpaId { get; set; }

        [JsonPropertyName("status")]
        public Status Status { get; set; }

        [JsonPropertyName("organizerId")]
        public int OrganizerId { get; set; }

        [JsonPropertyName("claimedBy")]
        public int? ClaimedBy { get; set; }

        [JsonProperty("tournamentPlayer")]
        public TournamentPlayer TournamentPlayer { get; set; }
    }
}
