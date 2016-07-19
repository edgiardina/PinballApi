using Newtonsoft.Json;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Players
{
    public class PlayerRecord
    {
        [JsonProperty("player")]
        public Player Player { get; set; }

        [JsonProperty("player_stats")]
        public PlayerStats PlayerStats { get; set; }

        [JsonProperty("championshipSeries")]
        public IList<ChampionshipSeries> ChampionshipSeries { get; set; }
    }
}
