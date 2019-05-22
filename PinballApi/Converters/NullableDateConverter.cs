using Newtonsoft.Json;
using System;

namespace PinballApi.Converters
{
    public class NullableDateConverter : Newtonsoft.Json.Converters.IsoDateTimeConverter
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && reader.Value.ToString().StartsWith("0000")) return null;
            else return base.ReadJson(reader, objectType, existingValue, serializer);
        }
    }
}
