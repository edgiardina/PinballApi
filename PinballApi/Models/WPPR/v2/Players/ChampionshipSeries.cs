using Newtonsoft.Json;

namespace PinballApi.Models.v2.WPPR
{
    public class ChampionshipSeries
    {
        [JsonProperty("region_code")]
        public string RegionCode { get; set; }

        [JsonProperty("series_code")]
        public string SeriesCode { get; set; }

        [JsonProperty("series_rank")]
        public int Rank { get; set; }

        [JsonProperty("total_points")]
        public double TotalPoints { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
    }
}
