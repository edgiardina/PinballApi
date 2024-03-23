using System.Text.Json.Serialization;
using System;
using System.Text.Json;

namespace PinballApi.Converters
{
    public class YesNoConverter : JsonConverter<bool>
    {
        public override bool Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) => reader.GetString()! == "Y";
        

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(bool);
        }

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
