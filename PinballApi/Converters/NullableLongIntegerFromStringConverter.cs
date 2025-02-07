using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PinballApi.Converters
{
    public class NullableLongIntegerFromStringConverter : JsonConverter<long?>
    {
        public override bool CanConvert(Type t) => t == typeof(long?);

        public override long? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();

            // TODO: 
            if (value == null || value == "Not Ranked")
                return null;

            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void Write(Utf8JsonWriter writer, long? value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.ToString(), options);
            return;
        }

        public static readonly LongIntegerFromStringConverter Singleton = new LongIntegerFromStringConverter();
    }
}
