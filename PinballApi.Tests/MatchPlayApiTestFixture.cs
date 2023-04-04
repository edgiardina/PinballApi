using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    [TestFixture]
    internal class MatchPlayApiTestFixture
    {
        private MatchPlayApi matchPlayApi;

        [SetUp]
        public void SetUp()
        {
            var t = new ConfigurationBuilder().AddUserSecrets<Settings>().Build();

            var apiToken = t["MatchPlayApiToken"];
            matchPlayApi = new MatchPlayApi(apiToken);
        }

        [Test]
        public async Task MatchPlayApi_GetDashboard_ShouldReturnDashboard()
        {
            var dashboard = await matchPlayApi.GetDashboard();

            Assert.That(dashboard.TournamentsOrganizing, Is.Not.Empty); 
        }

        [Test]
        public async Task MatchPlayApi_GetArenas_ShouldReturnArenas()
        {
            var arenas = await matchPlayApi.GetArenas();

            Assert.That(arenas, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_GetGames_ShouldReturnGames()
        {
            var games = await matchPlayApi.GetGames(new List<int> { 97597 });

            Assert.That(games, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_GetLocations_ShouldReturnLocations()
        {
            var locations = await matchPlayApi.GetLocations();
            
            Assert.That(locations, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_GetPlayers_ShouldReturnPlayers()
        {
            var players = await matchPlayApi.GetPlayers();

            Assert.That(players, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_GetMyProfile_ShouldMyProfile()
        {
            var profile = await matchPlayApi.GetMyProfile();

            Assert.That(profile, Is.Not.Null);
            Assert.That(profile.IfpaId == 16927);
        }

        [Test]
        public async Task MatchPlayApi_GetUserProfile_ShouldUserProfile()
        {
            var profile = await matchPlayApi.GetProfile(3259);

            Assert.That(profile, Is.Not.Null);
            Assert.That(profile.User.IfpaId == 16927);
        }

        [Test]
        public async Task MatchPlayApi_SearchForUser_ShouldReturnUser()
        {
            var userSearch = await matchPlayApi.SearchForUsers("Giardina");

            Assert.That(userSearch, Is.Not.Null);
            Assert.That(userSearch.Count == 2);
        }

        [Test]
        public async Task MatchPlayApi_SearchForTournament_ShouldReturnTournament()
        {
            var tournamentSearch = await matchPlayApi.SearchForTournaments("SFPD Spring 2020");

            Assert.That(tournamentSearch, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_GetSeries_ShouldReturnSeries()
        {
            var series = await matchPlayApi.GetSeries();

            Assert.That(series, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_GetSeries_ShouldReturnActiveSeries()
        {
            var series = await matchPlayApi.GetSeries(seriesStatus: Models.MatchPlay.SeriesStatus.Active);

            Assert.That(series, Is.Not.Empty);
            Assert.That(series.All(n => n.Status == Models.MatchPlay.SeriesStatus.Active));
        }

        [Test]
        public async Task MatchPlayApi_GetSeries_ShouldReturnCompletedSeries()
        {
            var series = await matchPlayApi.GetSeries(seriesStatus: Models.MatchPlay.SeriesStatus.Completed);

            Assert.That(series, Is.Not.Empty);
            Assert.That(series.All(n => n.Status == Models.MatchPlay.SeriesStatus.Completed));
        }

        [Test]
        public async Task MatchPlayApi_GetSeriesById_ShouldReturnSeries()
        {
            var series = await matchPlayApi.GetSeries(2175);

            Assert.That(series, Is.Not.Null);
            Assert.That(series.TournamentIds, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_GetSeriesAttendance_ShouldReturnSeriesAttendance()
        {
            var series = await matchPlayApi.GetSeriesAttendance(2216, 3);

            Assert.That(series, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_GetSeriesStats_ShouldReturnSeriesStats()
        {
            var seriesStatId = 2216;
            var series = await matchPlayApi.GetSeriesStats(seriesStatId);

            Assert.That(series, Is.Not.Null);
            Assert.That(series.Series.SeriesId, Is.EqualTo(seriesStatId));
        }

        [Test]
        public async Task MatchPlayApi_GetIfpaEstimate_ShouldReturnEstimate()
        {
            var estimate = await matchPlayApi.GetIfpaEstimate(touramentId: 80395);
            Assert.That(estimate, Is.Not.Null); 
        }

        [Test]
        public async Task MatchPlayApi_ComparePlayers_ShouldReturnComparison()
        {        
            var comparison = await matchPlayApi.ComparePlayers(ifpaIds: new List<int> { 16927, 2 });

            Assert.That(comparison, Is.Not.Null);
        }

        [Test]
        public async Task MatchPlayApi_GetRatingProfile_ShouldIfpaProfile()
        {
            var ifpaId = 16927;
            var profile = await matchPlayApi.GetRatingProfile(ifpaId, Models.MatchPlay.RatingQueryType.Ifpa);

            Assert.That(profile, Is.Not.Null);
            Assert.That(profile.Rating.IfpaId, Is.EqualTo(ifpaId));
        }

        [Test]
        public async Task MatchPlayApi_GetRatingProfile_ShouldUserProfile()
        {
            var ifpaId = 16927;
            var profile = await matchPlayApi.GetRatingProfile(ifpaId, Models.MatchPlay.RatingQueryType.Users);

            Assert.That(profile, Is.Not.Null);
            Assert.That(profile.Rating.UserId, Is.EqualTo(ifpaId));
        }

        [Test]
        public async Task MatchPlayApi_GetRatingPeriods_ShouldReturnPeriods()
        {
            var period = await matchPlayApi.GetRatingPeriods();

            Assert.That(period, Is.Not.Null);
            Assert.That(period, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_GetRatingPeriod_ShouldReturnPeriod()
        {
            var period = await matchPlayApi.GetRatingPeriod(new DateTime(2023,1,1));

            Assert.That(period, Is.Not.Null);
            Assert.That(period.Tournaments, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_GetCurrentRatingData_ShouldReturnRatingData()
        {
            var ifpaId = 16927;
            var period = await matchPlayApi.GetCurrentRatingData(ifpaIds: new List<int> { ifpaId, 2 });

            Assert.That(period, Is.Not.Null);
            Assert.That(period, Is.Not.Empty);
            Assert.True(period.Any(n => n.IfpaId == ifpaId));
        }

        [Test]
        public async Task MatchPlayApi_GetRatingHistoryByIfpaId_ShouldReturnRatingHistory()
        {
            var ifpaId = 16927;
            var count = 20;
            var history = await matchPlayApi.GetRatingHistoryByIfpaId(ifpaId, limit: count);

            Assert.That(history, Is.Not.Null);
            Assert.That(history, Is.Not.Empty);
            Assert.True(history.Count == count);
        }
    }
}
