using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Stats
{
    public class OverallStatistics
    {
        [JsonProperty("overall_player_count")]
        public int OverallPlayerCount { get; set; }

        [JsonProperty("active_player_count")]
        public int ActivePlayerCount { get; set; }

        [JsonProperty("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonProperty("tournament_count_last_month")]
        public int TournamentCountLastMonth { get; set; }

        [JsonProperty("tournament_count_this_year")]
        public int TournamentCountThisYear { get; set; }

        [JsonProperty("tournament_player_count")]
        public int TournamentPlayerCount { get; set; }

        [JsonProperty("tournament_player_count_average")]
        public double TournamentPlayerCountAverage { get; set; }
    }
}
