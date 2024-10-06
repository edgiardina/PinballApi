using PinballApi.Models.WPPR.Universal.Tournaments.Search;
using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace PinballApi.Converters
{
    public class TournamentSearchSortOrderConverter : JsonConverter<TournamentSearchSortOrder>
    {
        public override TournamentSearchSortOrder Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var enumValue = reader.GetString();

            if (enumValue == null)
            {
                throw new JsonException($"Unable to convert null to enum {typeof(TournamentSearchSortOrder)}.");
            }

            // Convert the string to the corresponding enum value
            return enumValue.ToUpper() switch
            {
                "ASC" => TournamentSearchSortOrder.Ascending,
                "DESC" => TournamentSearchSortOrder.Descending,
                _ => throw new JsonException($"Unknown value \"{enumValue}\" for enum {typeof(TournamentSearchSortOrder)}.")
            };
        }

        public override void Write(Utf8JsonWriter writer, TournamentSearchSortOrder value, JsonSerializerOptions options)
        {
            // Convert the enum value to the corresponding string (ASC or DESC)
            string stringValue = value switch
            {
                TournamentSearchSortOrder.Ascending => "ASC",
                TournamentSearchSortOrder.Descending => "DESC",
                _ => throw new JsonException($"Unknown enum value {value}.")
            };

            writer.WriteStringValue(stringValue);
        }
    }
}
