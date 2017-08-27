using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Rankings
{
    public class RankingList
    {
        [JsonProperty("total_count")]
        public string TotalCount { get; set; }

        [JsonProperty("sub_category")]
        public string SubCategory { get; set; }

        [JsonProperty("rankings")]
        public List<Ranking> Rankings { get; set; }
    }
}
