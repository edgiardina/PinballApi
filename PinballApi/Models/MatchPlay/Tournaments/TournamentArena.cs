using PinballApi.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    /// <summary>
    /// What one arena looks like inside a particular tournament.
    /// </summary>
    public class TournamentArena
    {
        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }

        /// <summary>
        /// Whether the organizer marked this arena as preferred.
        /// </summary>
        [JsonPropertyName("preferred")]
        [JsonConverter(typeof(TolerantNullableBooleanConverter))]
        public bool? Preferred { get; set; }

        [JsonPropertyName("scorbitVenuemachineId")]
        public int? ScorbitVenueMachineId { get; set; }

        [JsonPropertyName("scorbitVenuemachineUuid")]
        public string ScorbitVenueMachineUuid { get; set; }

        [JsonPropertyName("scorbitronInstalled")]
        [JsonConverter(typeof(TolerantNullableBooleanConverter))]
        public bool? ScorbitronInstalled { get; set; }

        /// <summary>
        /// The starting order of this arena in an Amazing Race tournament.
        /// </summary>
        [JsonPropertyName("amazingRaceSeed")]
        public int? AmazingRaceSeed { get; set; }

        [JsonPropertyName("bestGameBlocked")]
        public bool BestGameBlocked { get; set; }

        [JsonPropertyName("bestGameQueueClosed")]
        public bool BestGameQueueClosed { get; set; }

        [JsonPropertyName("labels")]
        public List<string> Labels { get; set; }

        [JsonPropertyName("labelColor")]
        public string LabelColor { get; set; }
    }
}
