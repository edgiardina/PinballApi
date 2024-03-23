using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsStatisticsByYear
    {
        [JsonPropertyName("year")]        
        public int Year { get; set; }

        [JsonPropertyName("stats")]
        public NacsStatistics Statistics { get; set; }

    }
}
