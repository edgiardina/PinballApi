using System;
using System.Text.Json.Serialization;
using System.Text.Json;
using Humanizer;

namespace PinballApi.Converters
{
    public class CapitalizedEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumValue = reader.GetString();

            if (enumValue == null)
            {
                throw new JsonException($"Unable to convert null to enum {typeof(T)}.");
            }

            // Convert the snake_case string (START_DATE) to PascalCase (StartDate)
            var pascalCaseValue = enumValue.Pascalize();

            // Try parsing the enum from the PascalCase string
            if (Enum.TryParse(pascalCaseValue, true, out T parsedEnum))
            {
                return parsedEnum;
            }

            throw new JsonException($"Unable to convert \"{enumValue}\" to enum {typeof(T)}.");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            // Convert the enum value (PascalCase) to snake_case (START_DATE)
            string enumString = value.ToString();
            if (!string.IsNullOrEmpty(enumString))
            {
                string snakeCase = enumString.Underscore().ToUpper(); // Ensure it's uppercase for consistency
                writer.WriteStringValue(snakeCase);
            }
        }
    }
}
