using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.WPPR.Universal.Series
{
    public class TournamentWinner
    {
        [JsonPropertyName("player_id")]
        public int PlayerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
