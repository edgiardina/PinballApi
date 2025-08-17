using PinballApi.Converters;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Tournaments.Related
{
    public class RelatedTournamentWinner
    {
        [JsonPropertyName("player_id")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long PlayerId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("country_name")]
        public string CountryName { get; set; }

        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }
    }
}
