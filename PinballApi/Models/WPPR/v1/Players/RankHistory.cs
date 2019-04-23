using Newtonsoft.Json;
using System;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class RankHistory
    {
        [JsonProperty("rank_date")]
        public DateTime RankDate { get; set; }

        [JsonProperty("rank_position")]
        public int RankPosition { get; set; }

        [JsonProperty("wppr_points")]
        public double WpprPoints { get; set; }
    }
}
