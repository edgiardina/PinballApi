using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsStateProvinceStatistics
    {
        [JsonProperty("month")]
        public int Month { get; set; }

        [JsonProperty("total_player_count")]
        public int TotalPlayerCount { get; set; }

        [JsonProperty("unique_player_count")]
        public int UniquePlayerCount { get; set; }

        [JsonProperty("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonProperty("prize_accumlation_value")]
        public double PrizeAccumlationValue { get; set; }
    }
}
