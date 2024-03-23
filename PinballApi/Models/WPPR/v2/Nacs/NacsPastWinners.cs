using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsPastWinners
    {
        [JsonPropertyName("stateprov_name")]
        public string StateProvince { get; set; }

        [JsonPropertyName("stateprov")]
        public string StateProvinceAbbreviation { get; set; }

        [JsonPropertyName("winners")]
        public List<NacsWinner> Winners { get; set; }
    }
}
