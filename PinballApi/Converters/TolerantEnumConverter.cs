using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Humanizer;

namespace PinballApi.Converters
{
    /// <summary>
    /// Reads a JSON string into an enum and falls back to <c>default(T)</c> when the value is
    /// null or is not a member of the enum. Use it for API fields that can gain new values later.
    /// The enum must declare a zero member that stands for "unknown".
    /// </summary>
    /// <typeparam name="T">The enum to convert.</typeparam>
    public class TolerantEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return default;
            }

            if (reader.TokenType == JsonTokenType.Number)
            {
                return (T)Enum.ToObject(typeof(T), reader.GetInt32());
            }

            var value = reader.GetString();

            if (string.IsNullOrWhiteSpace(value))
            {
                return default;
            }

            // The API sends camelCase ("machineGroup") or snake_case ("dots_animation").
            // Pascalize both into the shape the enum members use.
            if (Enum.TryParse(value.Pascalize(), true, out T parsed))
            {
                return parsed;
            }

            return default;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString().Camelize());
        }
    }
}
