using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PinballApi.Models.WPPR.Universal;
using PinballApi.Models.WPPR.Universal.Rankings;
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
        public async Task PinballRankingApi_TournamentSearch_GetSearchByEventType()
        {
            var result = await rankingApi.TournamentSearch(tournamentEventType: Models.WPPR.Universal.Tournaments.Search.TournamentEventType.League);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalResults, Is.GreaterThan(0));
            Assert.That(result.SearchFilter.EventType, Is.EqualTo(Models.WPPR.Universal.Tournaments.Search.TournamentEventType.League));
        }

        [Test]
        public async Task PinballRankingApi_TournamentSearch_EnsureSortingWorks()
        {
            var result = await rankingApi.TournamentSearch(tournamentSearchSortMode: Models.WPPR.Universal.Tournaments.Search.TournamentSearchSortMode.StartDate, tournamentSearchSortOrder: Models.WPPR.Universal.Tournaments.Search.TournamentSearchSortOrder.Descending);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalResults, Is.GreaterThan(0));
            Assert.That(result.Tournaments, Is.Not.Null);
            Assert.That(result.Tournaments.Length, Is.GreaterThan(0));
            Assert.That(result.Tournaments.First().EventStartDate, Is.GreaterThan(result.Tournaments.Last().EventStartDate));
        }

        [Test]
        [Ignore("There's a bug with onlyWithResults that needs to be fixed in the API")]
        public async Task PinballRankingApi_TournamentSearch_EnsureOnlyWithResultsWorks()
        {
            var result = await rankingApi.TournamentSearch(tournamentSearchSortMode: Models.WPPR.Universal.Tournaments.Search.TournamentSearchSortMode.StartDate, 
                                                           tournamentSearchSortOrder: Models.WPPR.Universal.Tournaments.Search.TournamentSearchSortOrder.Descending, 
                                                           onlyWithResults: true);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.TotalResults, Is.GreaterThan(0));
            Assert.That(result.Tournaments, Is.Not.Null);
            Assert.That(result.Tournaments.Length, Is.GreaterThan(0));
            Assert.That(result.Tournaments.All(t => t.PlayerCount > 0), Is.True);
        }

        [Test]
        public async Task PinballRankingApi_RankingSearch_GetRankingsByType([Values] RankingType type)
        {
            Assume.That(type, Is.Not.EqualTo(RankingType.Pro));

            var code = "US";

            var result = await rankingApi.RankingSearch(type, countryCode: code);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rankings, Is.Not.Null);
            Assert.That(result.Rankings.Count, Is.GreaterThan(0));
            Assert.That(result.RankingType, Is.EqualTo(type));
        }

        [Test]
        public async Task PinballRankingApi_ProRankingSearch_GetRankings([Values] TournamentType system)
        {
            Assume.That(system, Is.Not.EqualTo(TournamentType.Youth));

            var result = await rankingApi.ProRankingSearch(system);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Rankings, Is.Not.Null);
            Assert.That(result.Rankings.Count, Is.GreaterThan(0));
            Assert.That(result.RankingType, Is.EqualTo(RankingType.Pro));
        }

        [Test]
        public async Task PinballRankingApi_GetRankingCountries()
        {
            var result = await rankingApi.GetRankingCountries();

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Country, Is.Not.Null);
            Assert.That(result.Country.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task PinballRankingApi_GetRankingByCountry_ShouldThrowIfCountryCodeIsNullOrEmpty()
        {
            Assert.ThrowsAsync<ArgumentException>(async () => await rankingApi.RankingSearch(RankingType.Country));
        }

        [Test]
        public async Task PinballRankingApi_PlayerSearch_GetPlayerId()
        {
            var result = await rankingApi.GetPlayer(5363);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PlayerId, Is.EqualTo(5363));
        }

        [Test]
        public async Task PinballRankingApi_PlayerSearch_GetPlayerByName()
        {
            var result = await rankingApi.PlayerSearch("Raymond Davidson");

            Assert.That(result, Is.Not.Null);
            Assert.That(result.Results, Is.Not.Null);
            Assert.That(result.Results.Count, Is.GreaterThan(0));
            Assert.That(result.Results.First().FirstName, Is.EqualTo("Raymond"));
            Assert.That(result.Results.First().LastName, Is.EqualTo("Davidson"));
        }
    }
}
