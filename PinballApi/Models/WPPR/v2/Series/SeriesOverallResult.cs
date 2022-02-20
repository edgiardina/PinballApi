using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesOverallResult
    {
        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("region_name")]
        public string RegionName { get; set; }

        [JsonProperty("player_count")]
        public int PlayerCount { get; set; }

        [JsonProperty("unique_player_count")]
        public int UniquePlayerCount { get; set; }

        [JsonProperty("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonProperty("current_leader")]
        public CurrentLeader CurrentLeader { get; set; }

        [JsonProperty("prize_fund")]
        public double PrizeFund { get; set; }
    }
}
