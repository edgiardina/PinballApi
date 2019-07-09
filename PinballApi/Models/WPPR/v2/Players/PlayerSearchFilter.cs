using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Players
{
    public class PlayerSearchFilter
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("tournament")]
        public string Tournament { get; set; }

        [JsonProperty("tournament_position")]
        public string Tourpos { get; set; }
    }
}