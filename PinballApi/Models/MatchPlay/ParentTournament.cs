using System.Text.Json.Serialization;
using PinballApi.Models.MatchPlay.Tournaments;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay
{
    public class ParentTournament
    {
        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// The API sends this as a string, for example <c>completed</c>. Without the converter
        /// the whole tournament fails to deserialize.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TournamentStatus Status { get; set; }
    }
}
