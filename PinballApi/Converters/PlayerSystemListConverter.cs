using PinballApi.Models.WPPR.Universal.Players;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PinballApi.Converters
{
    public class PlayerSystemListConverter : JsonConverter<List<PlayerSystem>>
    {
        public override List<PlayerSystem> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var systems = new List<PlayerSystem>();

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected StartObject token");
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected PropertyName token");
                }

                string propertyName = reader.GetString().ToUpper();
                PlayerRankingSystem systemType = propertyName switch
                {
                    "MAIN" => PlayerRankingSystem.Main,
                    "OPEN" => PlayerRankingSystem.Main, // "OPEN" is a synonym for "MAIN"
                    "WOMENS" => PlayerRankingSystem.Women,
                    _ => throw new JsonException($"Unknown system type: {propertyName}")
                };

                reader.Read();
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException("Expected StartObject token");
                }

                var system = JsonSerializer.Deserialize<PlayerSystem>(ref reader, options);
                system.System = systemType;
                systems.Add(system);
            }

            return systems;
        }

        public override void Write(Utf8JsonWriter writer, List<PlayerSystem> value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().ToUpper());
        }
    }
}
