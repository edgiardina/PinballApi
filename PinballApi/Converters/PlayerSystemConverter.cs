using PinballApi.Models.WPPR.Universal.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace PinballApi.Converters
{
    public class PlayerSystemConverter : JsonConverter<List<PlayerSystem>>
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

                string propertyName = reader.GetString();
                PlayerRankingSystem systemType = propertyName switch
                {
                    "open" => PlayerRankingSystem.Open,
                    "womens" => PlayerRankingSystem.Women,
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
            writer.WriteStartObject();

            foreach (var system in value)
            {
                string propertyName = system.System switch
                {
                    PlayerRankingSystem.Open => "open",
                    PlayerRankingSystem.Women => "womens",
                    _ => throw new JsonException($"Unknown system type: {system.System}")
                };

                writer.WritePropertyName(propertyName);
                JsonSerializer.Serialize(writer, system, options);
            }

            writer.WriteEndObject();
        }
    }
}
