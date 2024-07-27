using PinballApi.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class PlayerStats
    {
        [JsonPropertyName("system")]
        [JsonConverter(typeof(PlayerSystemConverter))]
        public List<PlayerSystem> System { get; set; }

        [JsonPropertyName("years_active")]
        [JsonConverter(typeof(LongIntegerFromStringConverter))]
        public long YearsActive { get; set; }
    }
}
