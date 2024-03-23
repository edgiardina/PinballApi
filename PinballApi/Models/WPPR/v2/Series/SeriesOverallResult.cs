using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class SeriesOverallResult
    {
        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("region_name")]
        public string RegionName { get; set; }

        [JsonPropertyName("player_count")]
        public int PlayerCount { get; set; }

        [JsonPropertyName("unique_player_count")]
        public int UniquePlayerCount { get; set; }

        [JsonPropertyName("tournament_count")]
        public int TournamentCount { get; set; }

        [JsonPropertyName("current_leader")]
        public CurrentLeader CurrentLeader { get; set; }

        [JsonPropertyName("prize_fund")]
        public double PrizeFund { get; set; }
    }
}
