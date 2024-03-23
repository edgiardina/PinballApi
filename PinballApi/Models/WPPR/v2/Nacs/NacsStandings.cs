using System.Text.Json.Serialization;
using PinballApi.Models.WPPR.v2.Players;
using System;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsStandings
    {
        [JsonPropertyName("stateprov")]
        public string StateProvince { get; set; }

        [JsonPropertyName("stateprov_name")]
        public string StateProvinceName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("prize_value")]
        public double PrizeValue { get; set; }

        [JsonPropertyName("total_player_count")]
        public int TotalPlayerCount { get; set; }

        [JsonPropertyName("unique_player_count")]
        public int UniquePlayerCount { get; set; }

        [JsonPropertyName("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonPropertyName("current_leader")]
        public Player CurrentLeader { get; set; }
    }
}
