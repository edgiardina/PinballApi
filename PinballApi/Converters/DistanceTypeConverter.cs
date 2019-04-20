using Newtonsoft.Json;
using PinballApi.Models.WPPR.v2.Calendar;
using System;

namespace PinballApi.Converters
{

    internal class DistanceTypeConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(DistanceType) || t == typeof(DistanceType?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            if (value == "m")
            {
                return DistanceType.Miles;
            }

            if (value == "k")
            {
                return DistanceType.Kilometers;
            }
            throw new Exception("Cannot unmarshal type DistanceType");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (DistanceType)untypedValue;
            if (value == DistanceType.Miles)
            {
                serializer.Serialize(writer, "m");
                return;
            }

            if (value == DistanceType.Kilometers)
            {
                serializer.Serialize(writer, "k");
                return;
            }
            throw new Exception("Cannot marshal type DistanceType");
        }

        public static readonly DistanceTypeConverter Singleton = new DistanceTypeConverter();
    }

}
