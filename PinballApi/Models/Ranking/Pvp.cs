using Newtonsoft.Json;

namespace PinballApi.Models.Ranking
{
    public class Pvp
    {
        [JsonProperty("player_id")]
        public string PlayerId { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("win_count")]
        public string WinCount { get; set; }

        [JsonProperty("loss_count")]
        public string LossCount { get; set; }

        [JsonProperty("tie_count")]
        public string TieCount { get; set; }
    }
}
