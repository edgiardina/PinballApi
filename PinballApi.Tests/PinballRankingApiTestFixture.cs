using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    internal class PinballRankingApiTestFixture
    {

        private PinballRankingApi rankingApi;

        [SetUp]
        public void SetUp()
        {
            var t = new ConfigurationBuilder().AddUserSecrets<Settings>().Build();

            var apiKey = t["WPPRKey"];
            rankingApi = new PinballRankingApi(apiKey);
        }

        [Test]
        public async Task PinballRankingApi_TournamentSearch_GetSearchByLatLong()
        {
            var result = await rankingApi.TournamentSearch(41.8240, -71.4128, 150, Models.WPPR.v2.Calendar.DistanceType.Miles, startDate: DateTime.Now, endDate: DateTime.Now.AddYears(1));

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalResults, Is.GreaterThan(0));
            Assert.That(result.SearchFilter.Latitude, Is.EqualTo(41.8240));
            Assert.That(result.SearchFilter.Longitude, Is.EqualTo(-71.4128));
            Assert.That(result.SearchFilter.Radius, Is.EqualTo(150));
            Assert.That(result.SearchFilter.DistanceUnit, Is.EqualTo("m"));
        }

        [Test]
        public async Task PinballRankingApi_TournamentSearch_GetSearchById()
        {
            int tourneyId = 78504;
            var result = await rankingApi.GetTournament(tourneyId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TournamentId, Is.EqualTo(tourneyId));

        }

        [Test]
        public async Task PinballRankingApi_RankingSearch_GetRankingsByType()
        {
            var result = await rankingApi.RankingSearch(Models.WPPR.Universal.Rankings.RankingType.Pro, Models.WPPR.Universal.RankingSystem.Main);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rankings, Is.Not.Null);
            Assert.That(result.Rankings.Count, Is.GreaterThan(0));
            Assert.That(result.RankingType, Is.EqualTo(Models.WPPR.Universal.Rankings.RankingType.Pro));
        }
    }
}
