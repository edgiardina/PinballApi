using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.v2.Rankings
{
    public class CustomRankingList
    {
        [JsonProperty("total_count")]
        public int TotalCount { get; set; }

        [JsonProperty("custom_view")]
        public List<CustomRankingView> CustomRankingView { get; set; }
    }
}
