using PinballApi.Converters;
using System;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Tournaments.Search
{
    public class TournamentWinner
    {
        [JsonPropertyName("player_id")]
        [JsonConverter(typeof(NullableLongIntegerFromStringConverter))]
        public long? PlayerId { get; set; }

        [JsonPropertyName("player_name")]
        public string PlayerName { get; set; }

        [JsonPropertyName("wppr_points")]
        public string WpprPoints { get; set; }

        [JsonPropertyName("profile_url")]
        public Uri ProfileUrl { get; set; }
    }
}
