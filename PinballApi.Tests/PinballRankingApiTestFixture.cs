using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.Universal;
using PinballApi.Models.WPPR.Universal.Players;
using PinballApi.Models.WPPR.Universal.Rankings;
using System;
using System.Linq;
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
            var result = await rankingApi.TournamentSearch(41.8240, -71.4128, 150, DistanceType.Miles, startDate: DateTime.Now, endDate: DateTime.Now.AddYears(1));

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

        [Test]
        public async Task PinballRankingApi_PlayerResults_GetPlayerResults()
        {
            int playerId = 16927;

            var result = await rankingApi.GetPlayerResults(playerId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PlayerId, Is.EqualTo(playerId));
            Assert.That(result.Results, Is.Not.Null);
            Assert.That(result.Results.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task PinballRankingApi_PlayerHistory_GetPlayerHistory()
        {
            int playerId = 16927;

            var result = await rankingApi.GetPlayerHistory(playerId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PlayerId, Is.EqualTo(playerId));
            Assert.That(result.RankHistory, Is.Not.Null);
            Assert.That(result.RankHistory.Count, Is.GreaterThan(0));
            Assert.That(result.RatingHistory, Is.Not.Null);
            Assert.That(result.RatingHistory.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task PinballRankingApi_PlayerHistory_EnsureOnlyActiveResultsReturned()
        {
            int playerId = 16927;

            var result = await rankingApi.GetPlayerHistory(playerId, activeResultsOnly: true);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PlayerId, Is.EqualTo(playerId));
            Assert.That(result.RankHistory, Is.Not.Null);
            Assert.That(result.RankHistory.Count, Is.GreaterThan(0));
            // active results are only 36 months / 3 years or less
            Assert.That(result.RankHistory.Count, Is.LessThanOrEqualTo(36));
            Assert.That(result.RatingHistory, Is.Not.Null);
            Assert.That(result.RatingHistory.Count, Is.GreaterThan(0));
            Assert.That(result.ActiveFlag, Is.True);
        }

        [Test]
        public async Task PinballRankingApi_PlayerHistory_EnsureCorrectSystemReturned()
        {
            int playerId = 16927;
            var tourneyType = PlayerRankingSystem.Women;

            var result = await rankingApi.GetPlayerHistory(playerId, tourneyType);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PlayerId, Is.EqualTo(playerId));
            Assert.That(result.System, Is.EqualTo(tourneyType));
        }

        [Test]
        public async Task PinballRankingApi_PlayerVersus_GetPlayerVersus()
        {
            int playerId = 16927;

            var result = await rankingApi.GetPlayerVersusPlayer(playerId);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PlayerId, Is.EqualTo(playerId));
            Assert.That(result.PlayerVersusPlayerRecords, Is.Not.Null);
            Assert.That(result.PlayerVersusPlayerRecords.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task PinballRankingApi_PlayerVersus_GetPlayerVersusBySystem([Values] PlayerRankingSystem system)
        {
            // This endpoint doesn't work for Youth PVP records.
            Assume.That(system, Is.Not.EqualTo(PlayerRankingSystem.Youth));

            int playerId = 16927;
            var result = await rankingApi.GetPlayerVersusPlayer(playerId, system);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PlayerId, Is.EqualTo(playerId));
            Assert.That(result.System, Is.EqualTo(system));
        }

        [Test]
        public async Task PinballRankingApi_PlayerVersusComparison_GetComparison()
        {
            int playerId = 16927;
            int playerId2 = 5363;

            var result = await rankingApi.GetPlayerVersusPlayerComparison(playerId, playerId2);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PlayerOne.PlayerId, Is.EqualTo(playerId));
            Assert.That(result.PlayerTwo.PlayerId, Is.EqualTo(playerId2));
            Assert.That(result.ComparisonRecords, Is.Not.Null);
            Assert.That(result.ComparisonRecords.Count, Is.GreaterThan(0));
        }


        [Test]
        public async Task PinballRankingApi_PlayerSeriesCard_GetSeriesCard()
        {
            int playerId = 16927;

            var result = await rankingApi.GetSeriesPlayerCard(playerId, "NACS", "RI", 2023);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.PlayerId, Is.EqualTo(playerId));
            Assert.That(result.PlayerCard, Is.Not.Null);
            Assert.That(result.PlayerCard.Count, Is.GreaterThan(0));
        }


        [Test]
        public async Task PinballRankingApi_GetSeries()
        {
            var result = await rankingApi.GetSeries();

            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Empty);
            Assert.That(result.First().Years, Is.Not.Empty);
        }

        [Test]
        public async Task PinballRankingApi_GetSeriesTournamentsForRegion()
        {
            var seriesCode = "NACS";
            var regionCode = "RI";
            var result = await rankingApi.GetSeriesTournamentsForRegion(seriesCode, regionCode, 2023);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.SeriesCode, Is.EqualTo(seriesCode));
            Assert.That(result.RegionCode, Is.EqualTo(regionCode));
            Assert.That(result.SubmittedTournaments, Is.Not.Empty);
        }

        [Test]
        public async Task PinballRankingApi_GetSeriesStandingsForRegion()
        {
            var seriesCode = "NACS";
            var regionCode = "RI";
            var result = await rankingApi.GetSeriesStandingsForRegion(seriesCode, regionCode, 2023);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.SeriesCode, Is.EqualTo(seriesCode));
            Assert.That(result.RegionCode, Is.EqualTo(regionCode));
            Assert.That(result.Standings, Is.Not.Empty);
        }

        [Test]
        public async Task PinballRankingApi_GetSeriesOverallStanding()
        {
            var seriesCode = "NACS";
            var result = await rankingApi.GetSeriesOverallStanding(seriesCode, 2023);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.SeriesCode, Is.EqualTo(seriesCode));
            Assert.That(result.OverallResults, Is.Not.Empty);
        }

        [Test]
        public async Task PinballRankingApi_GetSeriesWinners()
        {
            var seriesCode = "NACS";
            var regionCode = "RI";
            var result = await rankingApi.GetSeriesWinners(seriesCode, regionCode);

            Assert.That(result, Is.Not.Null);
            Assert.That(result.SeriesCode, Is.EqualTo(seriesCode));
            Assert.That(result.RegionCode, Is.EqualTo(regionCode));
            Assert.That(result.Results, Is.Not.Empty);
        }
    }
}
