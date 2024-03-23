using System.Text.Json.Serialization;
using System;
using System.Text.Json;

namespace PinballApi.Converters
{
    /// <summary>
    /// For some data points, like Ratings Value or Ranking Value, we might see a string instead of a 
    /// datapoint; that string might read "not rated". Parse that value into a null.
    /// </summary>
    public class NotRatedNullableDescriptiveConverter : JsonConverter<double?>
    {
        private readonly string descriptiveTerm = "Not Rated";

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double?);
        }

        public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {            
            if (reader.TokenType == JsonTokenType.String)
            {
                string integerOrNullDescriptiveTerm = reader.GetString();

                if (integerOrNullDescriptiveTerm == descriptiveTerm)
                {
                    return null;
                }
                else
                {
                    return double.Parse(integerOrNullDescriptiveTerm);
                }
            }
            else if (reader.TokenType == JsonTokenType.Number)
            {
                return reader.GetDouble();
            }
            else
            {
                throw new JsonException($"Unsupported reader token type {reader.TokenType}");
            }
        }

        public override void Write(Utf8JsonWriter writer, double? value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
