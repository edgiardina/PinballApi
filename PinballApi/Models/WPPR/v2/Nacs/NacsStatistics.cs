using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsStatistics
    {
        [JsonPropertyName("year")]        
        public int Year { get; set; }

        [JsonPropertyName("stateprov_count")]
        public int StateProvinceCount { get; set; }

        [JsonPropertyName("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonPropertyName("unique_player_count")]
        public int UniquePlayerCount { get; set; }

        [JsonPropertyName("total_player_count")]
        public int TotalPlayerCount { get; set; }

        [JsonPropertyName("usa_player_count")]
        public int USAPlayerCount { get; set; }

        [JsonPropertyName("canada_player_count")]
        public int CanadaPlayerCount { get; set; }

        [JsonPropertyName("nationals_prize_value")]
        public double NationalsPrizeValue { get; set; }
    }
}
