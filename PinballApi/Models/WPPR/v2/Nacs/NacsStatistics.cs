using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsStatistics
    {
        [JsonProperty("year")]        
        public int Year { get; set; }

        [JsonProperty("stateprov_count")]
        public int StateProvinceCount { get; set; }

        [JsonProperty("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonProperty("unique_player_count")]
        public int UniquePlayerCount { get; set; }

        [JsonProperty("total_player_count")]
        public int TotalPlayerCount { get; set; }

        [JsonProperty("usa_player_count")]
        public int USAPlayerCount { get; set; }

        [JsonProperty("canada_player_count")]
        public int CanadaPlayerCount { get; set; }

        [JsonProperty("nationals_prize_value")]
        public double NationalsPrizeValue { get; set; }
    }
}
