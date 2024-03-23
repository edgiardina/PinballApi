using System.Text.Json.Serialization;

namespace PinballApi.Models.v2.WPPR
{
    public class ChampionshipSeries
    {
        [JsonPropertyName("region_code")]
        public string RegionCode { get; set; }

        [JsonPropertyName("series_code")]
        public string SeriesCode { get; set; }

        [JsonPropertyName("series_rank")]
        public int Rank { get; set; }

        [JsonPropertyName("total_points")]
        public double TotalPoints { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }
    }
}
