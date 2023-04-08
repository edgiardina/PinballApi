using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class FlipFrenzy
    {
        [JsonProperty("games")]
        public List<TournamentGame> Games { get; set; }

        [JsonProperty("queue")]
        public List<Queue> Queue { get; set; }

        [JsonProperty("avgQueueDuration")]
        public int AvgQueueDuration { get; set; }
    }
}
