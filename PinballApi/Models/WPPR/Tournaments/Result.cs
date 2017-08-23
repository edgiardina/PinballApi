﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Tournaments
{
    public class Result
    {
        [JsonProperty("tournament_name")]
        public string TournamentName { get; set; }

        [JsonProperty("tournament_id")]
        public string TournamentId { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("periodic_flag")]
        public string PeriodicFlag { get; set; }

        [JsonProperty("wppr_state")]
        public string WpprState { get; set; }

        [JsonProperty("event")]
        public IList<Event> Event { get; set; }
    }
}
