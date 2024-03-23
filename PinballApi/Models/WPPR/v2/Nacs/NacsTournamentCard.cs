using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace PinballApi.Models.WPPR.v2.Nacs
{
    public class NacsTournamentCard
    {
        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("stateprov_code")]
        public string StateProvince { get; set; }

        [JsonPropertyName("stateprov_name")]
        public string StateProvinceName { get; set; }

        [JsonPropertyName("player_name")]
        public string PlayerName { get; set; }

        [JsonPropertyName("nacs_tournament_card")]
        public List<NacsTournamentCardRecord> NacsTournamentCardRecords { get; set; }
    }
}
