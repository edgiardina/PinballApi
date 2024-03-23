using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class Tournament
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_date")]
        public DateTime EventDate { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }
    }
}
