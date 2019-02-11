using Newtonsoft.Json;
using System;

namespace PinballApi.Converters
{
    public class YesNoConverter : JsonConverter
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
                    string booleanString = reader.Value.ToString();

                    return booleanString == "Y";
                }               
                else
                {
                    throw new JsonSerializationException($"Unsupported reader token type {reader.TokenType}");
                }
            }
            catch (Exception ex)
            {
                throw new JsonSerializationException("Error converting value to type bool.", ex);
            }
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }
    }
}
