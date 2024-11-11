using System.Text.Json.Serialization;
using System;
using System.Text.Json;
using PinballApi.Models.WPPR;

namespace PinballApi.Converters
{
    internal class DistanceTypeConverter : JsonConverter<DistanceType>
    {
        public override DistanceType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
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

        public override void Write(Utf8JsonWriter writer, DistanceType value, JsonSerializerOptions options)
        {
            if (value == DistanceType.Miles)
            {
                writer.WriteStringValue("m");
                return;
            }

            if (value == DistanceType.Kilometers)
            {
                writer.WriteStringValue("k");
                return;
            }
            throw new Exception("Cannot marshal type DistanceType");
        }
    }
}