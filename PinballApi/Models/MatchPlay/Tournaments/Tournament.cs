using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class Tournament
    {
        [JsonPropertyName("tournamentId")]
        public int TournamentId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TournamentStatus Status { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("startUtc")]
        public DateTime StartUtc { get; set; }

        [JsonPropertyName("startLocal")]
        public string StartLocal { get; set; }

        [JsonPropertyName("completedAt")]
        public DateTime? CompletedAt { get; set; }

        [JsonPropertyName("organizerId")]
        public int OrganizerId { get; set; }

        [JsonPropertyName("locationId")]
        public int? LocationId { get; set; }

        [JsonPropertyName("location")]
        public Location Location { get; set; }

        [JsonPropertyName("seriesId")]
        public int? SeriesId { get; set; }

        [JsonPropertyName("series")]
        public Series Series { get; set; }

        [JsonPropertyName("description")]
        public object Description { get; set; }

        [JsonPropertyName("pointsMap")]
        public List<List<decimal>> PointsMap { get; } = new List<List<decimal>>();

        [JsonPropertyName("tiebreakerPointsMap")]
        public List<List<decimal>> TiebreakerPointsMap { get; } = new List<List<decimal>>();

        [JsonPropertyName("test")]
        public bool Test { get; set; }

        [JsonPropertyName("timezone")]
        public string Timezone { get; set; }

        [JsonPropertyName("scorekeeping")]
        public string Scorekeeping { get; set; }

        [JsonPropertyName("scorekeepers")]
        public List<Scorekeeper> Scorekeepers { get; set; }

        [JsonPropertyName("link")]
        public object Link { get; set; }

        [JsonPropertyName("linkedTournamentId")]
        public int? LinkedTournamentId { get; set; }

        [JsonPropertyName("estimatedTgp")]
        public int? EstimatedTgp { get; set; }

        [JsonPropertyName("organizer")]
        public User Organizer { get; set; }

        [JsonPropertyName("seeding")]
        public string Seeding { get; set; }

        [JsonPropertyName("firstRoundPairing")]
        public string FirstRoundPairing { get; set; }

        [JsonPropertyName("pairing")]
        public string Pairing { get; set; }

        [JsonPropertyName("playerOrder")]
        public string PlayerOrder { get; set; }

        [JsonPropertyName("arenaAssignment")]
        public string ArenaAssignment { get; set; }

        [JsonPropertyName("duration")]
        public int Duration { get; set; }

        [JsonPropertyName("gamesPerRound")]
        public int GamesPerRound { get; set; }

        [JsonPropertyName("suggestions")]
        public string Suggestions { get; set; }

        [JsonPropertyName("tiebreaker")]
        public string Tiebreaker { get; set; }

        [JsonPropertyName("scoring")]
        public string Scoring { get; set; }

        [JsonPropertyName("arenas")]
        public List<Arena> Arenas { get; set; }

        [JsonPropertyName("players")]
        public List<Player> Players { get; set; }

        /// <summary>
        /// The tournament this one hangs off. Set only when the caller asks for the parent.
        /// </summary>
        [JsonPropertyName("parentTournament")]
        public ParentTournament ParentTournament { get; set; }

        /// <summary>
        /// The playoffs or finals that follow this tournament. Set only when the caller asks for
        /// the playoffs, and null when the tournament has none.
        /// </summary>
        [JsonPropertyName("playoffsTournament")]
        public ParentTournament PlayoffsTournament { get; set; }

        /// <summary>
        /// The qualifying and playoff tournaments tied to this one. Set only when the caller asks
        /// for the linked tournaments. The API returns an empty list rather than null.
        /// </summary>
        [JsonPropertyName("linkedTournaments")]
        public List<ParentTournament> LinkedTournaments { get; set; }

        //TODO: include banks. The API returns a "banks" array but it is empty on every tournament
        //      checked so far, so the item shape is still unknown.
        //TODO: include rsvp configuration. The API returns no extra key for it on any tournament
        //      checked so far.
    }
}
