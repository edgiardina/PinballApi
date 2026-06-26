using NUnit.Framework;
using PinballApi.Models.WPPR.Universal.Tournaments;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PinballApi.Tests
{
    /// <summary>
    /// Pure deserialization tests for the Tournament model. These do not hit the live API and
    /// require no secrets/network — they exercise the same JsonSerializerOptions the API uses
    /// (see BasePinballRankingApi) against captured/synthetic payloads.
    /// </summary>
    internal class TournamentDeserializationTests
    {
        // Mirrors BasePinballRankingApi.JsonSerializerOptions
        private static readonly JsonSerializerOptions ApiJsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            Converters =
            {
                new JsonStringEnumConverter(JsonNamingPolicy.CamelCase, false)
            }
        };

        // Represents an unrated event as returned by GET /tournament/{id}. For such events the
        // IFPA API returns null for the computed strength/value/grade metrics. System.Text.Json
        // throws on the first null it encounters ($.ratings_strength), so the sibling metrics are
        // also modeled as null here to prove the whole class of field is handled.
        private const string UnratedTournamentJson = """
            {
                "tournament_id": "116305",
                "tournament_name": "Unrated Event",
                "event_weight": 1.0,
                "ratings_strength": null,
                "rankings_strength": null,
                "base_value": null,
                "tournament_percentage_grade": null,
                "tournament_value": null
            }
            """;

        [Test]
        public void Tournament_Deserialize_UnratedEvent_WithNullMetrics_DoesNotThrow()
        {
            // Before the fix this throws:
            //   JsonException: DeserializeUnableToConvertValue, System.Double Path: $.ratings_strength
            //   ---> InvalidOperationException: InvalidCast, Null, number
            var result = JsonSerializer.Deserialize<Tournament>(UnratedTournamentJson, ApiJsonOptions);

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.TournamentId, Is.EqualTo(116305));
            Assert.That(result.TournamentName, Is.EqualTo("Unrated Event"));

            Assert.That(result.RatingsStrength, Is.Null);
            Assert.That(result.RankingsStrength, Is.Null);
            Assert.That(result.BaseValue, Is.Null);
            Assert.That(result.TournamentPercentageGrade, Is.Null);
            Assert.That(result.TournamentValue, Is.Null);
        }
    }
}
