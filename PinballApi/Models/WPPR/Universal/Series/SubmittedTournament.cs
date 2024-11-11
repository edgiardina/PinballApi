using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class SubmittedTournament
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateOnly EventEndDate { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonPropertyName("winner")]
        public TournamentWinner Winner { get; set; }

        [JsonPropertyName("player_count")]
        public int PlayerCount { get; set; }
    }
}
