using PinballApi.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PinballApi.Models.WPPR.Universal.Players
{
    public class PlayerStats
    {
        [JsonPropertyName("system")]
        [JsonConverter(typeof(PlayerSystemListConverter))]
        public List<PlayerSystem> System { get; set; }

        [JsonPropertyName("years_active")]
        [JsonConverter(typeof(NullableLongIntegerFromStringConverter))]
        public long? YearsActive { get; set; }

        public PlayerSystem Open
        {
            get
            {
                return System.Find(x => x.System == PlayerRankingSystem.Main);
            }
        }
    }
}
