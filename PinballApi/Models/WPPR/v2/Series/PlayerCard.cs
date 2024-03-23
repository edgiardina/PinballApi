using System.Text.Json.Serialization;
using System;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class PlayerCard
    {
        [JsonPropertyName("tournament_id")]
        public int TournamentId { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_name")]
        public string EventName { get; set; }

        [JsonPropertyName("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonPropertyName("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonPropertyName("region_event_rank")]
        public int RegionEventRank { get; set; }
    }
}
