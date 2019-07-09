using Newtonsoft.Json;
using PinballApi.Models.WPPR.v2.Players;
using System;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsStandings
    {
        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("stateprov_name")]
        public string StateProvinceName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("prize_value")]
        public double PrizeValue { get; set; }

        [JsonProperty("total_player_count")]
        public int TotalPlayerCount { get; set; }

        [JsonProperty("unique_player_count")]
        public int UniquePlayerCount { get; set; }

        [JsonProperty("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonProperty("current_leader")]
        public Player CurrentLeader { get; set; }
    }
}
