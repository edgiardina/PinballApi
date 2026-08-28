using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PinballApi.Converters
{
    /// <summary>
    /// Reads a nullable boolean that the API may send as a boolean, as a string, or as 0 or 1.
    /// </summary>
    /// <remarks>
    /// Use this for a flag that is always null in the responses seen so far, so the underlying
    /// representation is not confirmed. The converter accepts every reasonable form instead of
    /// throwing when the real value finally appears.
    /// </remarks>
    public class TolerantNullableBooleanConverter : JsonConverter<bool?>
    {
        public override bool? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            switch (reader.TokenType)
            {
                case JsonTokenType.Null:
                    return null;

                case JsonTokenType.True:
                    return true;

                case JsonTokenType.False:
                    return false;

                case JsonTokenType.Number:
                    return reader.GetDouble() != 0;

                case JsonTokenType.String:
                    var value = reader.GetString();

                    if (string.IsNullOrWhiteSpace(value))
                    {
                        return null;
                    }

                    if (bool.TryParse(value, out var parsed))
                    {
                        return parsed;
                    }

                    if (double.TryParse(value, out var number))
                    {
                        return number != 0;
                    }

                    return null;

                default:
                    reader.Skip();
                    return null;
            }
        }

        public override void Write(Utf8JsonWriter writer, bool? value, JsonSerializerOptions options)
        {
            if (value.HasValue)
            {
                writer.WriteBooleanValue(value.Value);
            }
            else
            {
                writer.WriteNullValue();
            }
        }
    }
}
