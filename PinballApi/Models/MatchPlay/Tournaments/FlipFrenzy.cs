using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class FlipFrenzy
    {
        [JsonPropertyName("games")]
        public List<TournamentGame> Games { get; set; }

        [JsonPropertyName("queue")]
        public List<Queue> Queue { get; set; }

        [JsonPropertyName("avgQueueDuration")]
        public int AvgQueueDuration { get; set; }
    }
}
