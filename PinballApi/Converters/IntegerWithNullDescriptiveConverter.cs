using Newtonsoft.Json;
using System;

namespace PinballApi.Converters
{
    /// <summary>
    /// For some data points, like Ratings Value or Ranking Value, we might see a string instead of a 
    /// datapoint; that string might read "not rated". Parse that value into a null.
    /// </summary>
    public class IntegerWithNullDescriptiveConverter : JsonConverter
    {
        private readonly string descriptiveTerm;

        public IntegerWithNullDescriptiveConverter(string descriptiveTerm)
        {
            this.descriptiveTerm = descriptiveTerm;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try
            {
                if (reader.TokenType == JsonToken.String)
                {
                    string integerOrNullDescriptiveTerm = reader.Value.ToString();

                    if (integerOrNullDescriptiveTerm == descriptiveTerm)
                    {
                        return null;
                    }
                    else
                    {
                        return int.Parse(integerOrNullDescriptiveTerm);
                    }
                }
                else if (reader.TokenType == JsonToken.Integer)
                {
                    return reader.Value;
                }
                else
                {
                    throw new JsonSerializationException($"Unsupported reader token type {reader.TokenType}");
                }
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException("Error converting value to type int?.", ex);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(int?);
        }
    }
}
