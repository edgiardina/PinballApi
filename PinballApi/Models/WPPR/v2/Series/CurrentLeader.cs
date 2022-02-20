using Newtonsoft.Json;

namespace PinballApi.Models.WPPR.v2.Series
{
    public class CurrentLeader
    {
        [JsonProperty("player_id")]
        public int PlayerId { get; set; }

        [JsonProperty("player_name")]
        public string PlayerName { get; set; }
    }
}
