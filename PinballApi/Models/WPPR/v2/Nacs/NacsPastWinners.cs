using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsPastWinners
    {
        [JsonProperty("stateprov")]
        public string StateProvince { get; set; }

        [JsonProperty("info")]
        public List<NacsWinner> Winners { get; set; }
    }
}
