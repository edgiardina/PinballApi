﻿using Microsoft.Extensions.Configuration;
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
    }
}
