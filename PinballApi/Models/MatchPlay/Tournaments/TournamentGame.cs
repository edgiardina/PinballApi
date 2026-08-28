using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    /// <summary>
    /// A game with the player objects attached. Every other member comes from <see cref="Game"/>.
    /// </summary>
    public class TournamentGame : Game
    {
        [JsonPropertyName("players")]
        public List<Player> Players { get; set; }
    }
}
