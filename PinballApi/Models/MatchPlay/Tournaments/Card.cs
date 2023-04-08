using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Models.MatchPlay.Tournaments
{
    public class Card
    {
        [JsonProperty("cardId")]
        public int CardId { get; set; }

        [JsonProperty("tournamentId")]
        public int TournamentId { get; set; }

        [JsonProperty("playerId")]
        public int PlayerId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("bestGame")]
        public bool BestGame { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("singlePlayerGames")]
        public List<SinglePlayerGame> SinglePlayerGames { get; set; }

        [JsonProperty("singlePlayerGameIds")]
        public List<int> SinglePlayerGameIds { get; set; }

        [JsonProperty("arenaIds")]
        public List<int> ArenaIds { get; set; }

        [JsonProperty("points")]
        public int Points { get; set; }
    }
}
