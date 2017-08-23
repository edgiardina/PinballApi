using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.Tournaments
{
    public class TournamentRequest
    {
        [JsonProperty("tournament")]
        public Tournament Tournament { get; set; }
    }
}
