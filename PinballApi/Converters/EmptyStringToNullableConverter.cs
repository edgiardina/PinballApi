using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PinballApi.Converters
{
    /// <summary>
    /// Generic converter that converts empty strings to null for nullable value types.
    /// This should be used for simple properties that only need empty string -> null conversion.
    /// For properties that might contain numeric strings or descriptive text, 
    /// use EmptyStringNullableIntDescriptiveConverter or EmptyStringNullableDoubleDescriptiveConverter instead.
    /// </summary>
    /// <typeparam name="T">The nullable value type to convert (e.g., int?, double?, decimal?).</typeparam>
    public class EmptyStringToNullableConverter<T> : JsonConverter<T?> where T : struct
    {
        public override bool CanConvert(Type typeToConvert) => typeToConvert == typeof(T?);

        public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                if (string.IsNullOrWhiteSpace(stringValue))
                {
                    return null;
                }

                // For simple cases, we don't try to parse strings - just return null
                // Use EmptyStringNullable*DescriptiveConverter for properties that need string parsing
                return null;
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                // Handle numeric JSON tokens directly
                if (typeof(T) == typeof(int))
                {
                    return (T?)(object)reader.GetInt32();
                }
                else if (typeof(T) == typeof(double))
                {
                    return (T?)(object)reader.GetDouble();
                }
                else if (typeof(T) == typeof(decimal))
                {
                    return (T?)(object)reader.GetDecimal();
                }
                else if (typeof(T) == typeof(float))
                {
                    return (T?)(object)reader.GetSingle();
                }
                else if (typeof(T) == typeof(long))
                {
                    return (T?)(object)reader.GetInt64();
                }
            }
            else if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            throw new JsonException($"Unsupported reader token type {reader.TokenType} for type {typeof(T)}");
        }

        public override void Write(Utf8JsonWriter writer, T? value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
            }
            else if (typeof(T) == typeof(int))
            {
                writer.WriteNumberValue((int)(object)value);
            }
            else if (typeof(T) == typeof(double))
            {
                writer.WriteNumberValue((double)(object)value);
            }
            else if (typeof(T) == typeof(decimal))
            {
                writer.WriteNumberValue((decimal)(object)value);
            }
            else if (typeof(T) == typeof(float))
            {
                writer.WriteNumberValue((float)(object)value);
            }
            else if (typeof(T) == typeof(long))
            {
                writer.WriteNumberValue((long)(object)value);
            }
            else
            {
                // Fallback to string representation
                writer.WriteStringValue(value.ToString());
            }
        }
    }
}