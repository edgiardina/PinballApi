using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.Ranking.Statistics
{
    public class PointsThisYear
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("stats")]
        public IList<PointsThisYearStat> Stats { get; set; }
    }
}
