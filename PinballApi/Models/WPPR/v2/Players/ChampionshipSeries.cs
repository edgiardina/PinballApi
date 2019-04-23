using Newtonsoft.Json;

namespace PinballApi.Models.v2.WPPR
{
    public class ChampionshipSeries
    {
        [JsonProperty("group_code")]
        public string GroupCode { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("series_rank")]
        public int Rank { get; set; }

        [JsonProperty("series_name")]
        public string SeriesName { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
    }
}
