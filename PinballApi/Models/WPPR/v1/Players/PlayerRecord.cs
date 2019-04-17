using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v1.Players
{
    public class PlayerRecord
    {
        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("player_stats")]
        public PlayerStats PlayerStats { get; set; }

        [JsonProperty("championshipSeries")]
        public List<ChampionshipSeries> ChampionshipSeries { get; set; }
    }
}
