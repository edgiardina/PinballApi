using Newtonsoft.Json;

namespace PinballApi.Models.Ranking
{
    public class ChampionshipSeries
    {
        [JsonProperty("view_id")]
        public string ViewId { get; set; }

        [JsonProperty("group_code")]
        public string GroupCode { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("rank")]
        public string Rank { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }
    }
}
