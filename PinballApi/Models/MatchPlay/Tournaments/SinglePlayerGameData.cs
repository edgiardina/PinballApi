using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class SinglePlayerGameData
    {
        [JsonPropertyName("voided")]
        public int Voided { get; set; }
        [JsonPropertyName("completed")]
        public int Completed { get; set; }
        [JsonPropertyName("best")]
        public int Best { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("playerId")]
        public int PlayerId { get; set; }
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}