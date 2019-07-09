using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsStatisticsByYear
    {
        [JsonProperty("year")]        
        public int Year { get; set; }

        [JsonProperty("stats")]
        public NacsStatistics Statistics { get; set; }

    }
}
