using Newtonsoft.Json;

namespace PinballApi.Models.v2.WPPR
{
    public class ChampionshipSeries
    {
        [JsonProperty("stateprov_code")]
        public string StateProvince { get; set; }

        [JsonProperty("stateprov")]
        public string StateProvinceName { get; set; }

        [JsonProperty("current_rank")]
        public int Rank { get; set; }

        [JsonProperty("year")]
        public int Year { get; set; }
    }
}
