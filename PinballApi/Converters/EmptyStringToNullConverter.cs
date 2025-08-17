using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PinballApi.Converters
{
    /// <summary>
    /// Generic converter that converts empty strings to null for any reference type.
    /// When JSON contains an empty string for a property that should be a complex object,
    /// this converter will return null instead of attempting to deserialize the empty string.
    /// </summary>
    /// <typeparam name="T">The reference type to convert. Must be a class (reference type).</typeparam>
    public class EmptyStringToNullConverter<T> : JsonConverter<T> where T : class
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(T);

        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    return null;
                }
                
                // If it's a non-empty string but we're expecting a complex object,
                // we should probably return null as well since it's likely invalid
                return null;
            }
            else if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }
            
            // For non-string, non-null values, deserialize normally
            return JsonSerializer.Deserialize<T>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else
            {
                JsonSerializer.Serialize(writer, value, options);
            }
        }
    }
}