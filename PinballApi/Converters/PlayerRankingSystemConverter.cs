using PinballApi.Models.WPPR.Universal.Players;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PinballApi.Converters
{
    public class PlayerRankingSystemConverter : JsonConverter<PlayerRankingSystem>
    {
        public override PlayerRankingSystem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumString = reader.GetString().ToLower();
            enumString = enumString.ToUpper();
            return (PlayerRankingSystem)Enum.Parse(typeToConvert, enumString, ignoreCase: true);
        }

        public override void Write(Utf8JsonWriter writer, PlayerRankingSystem value, JsonSerializerOptions options)
        {
            string enumString = value switch
            {
                PlayerRankingSystem.Main => "MAIN",
                PlayerRankingSystem.Women => "WOMEN",
                PlayerRankingSystem.Youth => "YOUTH",
                _ => throw new JsonException($"Unknown system type: {value}")
            };

            writer.WriteStringValue(enumString);
        }
    }
}
