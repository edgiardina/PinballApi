﻿using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v1.Pvp
{
    public class Pvp
    {
        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("tournament_id")]
        public string TournamentId { get; set; }

        [JsonProperty("event_date")]
        public string EventDate { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        [JsonProperty("tournament_country_code")]
        public string TournamentCountryCode { get; set; }

        [JsonProperty("p1_finish_position")]
        public int P1FinishPosition { get; set; }

        [JsonProperty("p2_finish_position")]
        public int P2FinishPosition { get; set; }
    }
}
