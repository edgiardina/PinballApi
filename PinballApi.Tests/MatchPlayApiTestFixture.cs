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
        public async Task MatchPlayApi_GetUserProfile_ShouldUserProfile()
        {
            var profile = await matchPlayApi.GetProfile(3259);

            Assert.That(profile, Is.Not.Null);
            Assert.That(profile.User.IfpaId == 16927);
        }

        [Test]
        public async Task MatchPlayApi_SearchForUser_ShouldReturnUser()
        {
            var userSearch = await matchPlayApi.SearchForUser("Giardina");

            Assert.That(userSearch, Is.Not.Null);
            Assert.That(userSearch.Count == 2);
        }

        [Test]
        public async Task MatchPlayApi_SearchForTournament_ShouldReturnTournament()
        {
            var tournamentSearch = await matchPlayApi.SearchForTournaments("SFPD Spring 2020");

            Assert.That(tournamentSearch, Is.Not.Empty);
        }
    }
}
