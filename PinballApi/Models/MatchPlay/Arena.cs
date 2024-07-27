using System.Text.Json.Serialization;
using PinballApi.Models.MatchPlay.Tournaments;

namespace PinballApi.Models.MatchPlay
{
    public class Arena
    {
        [JsonPropertyName("arenaId")]
        public int ArenaId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }

        [JsonPropertyName("opdbId")]
        public string OpdbId { get; set; }

        [JsonPropertyName("categoryId")]
        public int? CategoryId { get; set; }

        [JsonPropertyName("organizerId")]
        public int OrganizerId { get; set; }

        [JsonPropertyName("tournamentArena")]
        public TournamentArena TournamentArena { get; set; }
    }
}
