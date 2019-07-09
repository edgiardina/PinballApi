using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsPastWinners
    {
        [JsonProperty("stateprov_name")]
        public string StateProvince { get; set; }

        [JsonProperty("stateprov")]
        public string StateProvinceAbbreviation { get; set; }

        [JsonProperty("winners")]
        public List<NacsWinner> Winners { get; set; }
    }
}
