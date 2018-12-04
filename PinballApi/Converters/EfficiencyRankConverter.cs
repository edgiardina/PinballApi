using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace PinballApi.Converters
{
    public class EfficiencyRankConverter : JsonConverter
    {
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
                    string integerOrNotRanked = reader.Value.ToString();

                    if (integerOrNotRanked == "Not Ranked")
                    {
                        return null;
                    }
                    else
                    {
                        return int.Parse(integerOrNotRanked);
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
