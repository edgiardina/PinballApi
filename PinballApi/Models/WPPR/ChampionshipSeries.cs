using Newtonsoft.Json;

namespace PinballApi.Models.WPPR
{
    public class ChampionshipSeries
    {
        [JsonProperty("view_id")]
        public int ViewId { get; set; }

        [JsonProperty("group_code")]
        public string GroupCode { get; set; }

        [JsonProperty("group_name")]
        public string GroupName { get; set; }

        [JsonProperty("rank")]
        public int Rank { get; set; }

        [JsonProperty("country_name")]
        public string CountryName { get; set; }
    }
}
