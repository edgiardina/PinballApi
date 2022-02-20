using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class PlayerCard
    {
        [JsonProperty("tournament_id")]
        public int TournamentId { get; set; }

        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("event_end_date")]
        public DateTime EventEndDate { get; set; }

        [JsonProperty("wppr_points")]
        public double WpprPoints { get; set; }

        [JsonProperty("region_event_rank")]
        public int RegionEventRank { get; set; }
    }
}
