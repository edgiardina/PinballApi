﻿using System.Text.Json.Serialization;
using System;
using System.Text.Json;
using System.Globalization;

namespace PinballApi.Converters
{
    /// <summary>
    /// For some data points, like Ratings Value or Ranking Value, we might see a string instead of a 
    /// datapoint; that string might read "not rated". Parse that value into a null.
    /// </summary>
    public class EmptyStringNullableDoubleDescriptiveConverter : JsonConverter<double?>
    {

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(double?);
        }

        public override double? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {            
            if (reader.TokenType == JsonTokenType.String)
            {
                string integerOrNullDescriptiveTerm = reader.GetString();

                if (String.IsNullOrWhiteSpace(integerOrNullDescriptiveTerm))
                {
                    return null;
                }
                else
                {
                    return double.Parse(integerOrNullDescriptiveTerm, CultureInfo.InvariantCulture);
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
            if (value == null)
            {
                writer.WriteStringValue(string.Empty);
            }
            else
            {
                writer.WriteNumberValue(value.Value);
            }
        }
    }
}
