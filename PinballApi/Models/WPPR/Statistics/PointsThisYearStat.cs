using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.Statistics
{
    public class PointsThisYearStat
    {
        [JsonProperty("player_id")]
        public string PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("wppr_points")]
        public string WpprPoints { get; set; }

        [JsonProperty("stats_rank")]
        public int StatsRank { get; set; }
    }
}
