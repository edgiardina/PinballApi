using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PinballApi.Models.WPPR.v2.Players;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    [TestFixture]
    class PinballRankingApiV2TestFixture
    {
        private PinballRankingApiV2 rankingApi;
        private const int EdGiardinaPlayerId = 16927;

        [SetUp]
        public void SetUp()
        {
            var t = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

            var apiKey = t["WPPRKey"];
            rankingApi = new PinballRankingApiV2(apiKey);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayer_ShouldReturnCorrectPlayer()
        {
            var player = await rankingApi.GetPlayer(EdGiardinaPlayerId);

            Assert.That(player.FirstName == "Ed");
            //To make sure unranked player changes didn't ruin ranked player efficiency stats
            Assert.That(player.PlayerStats.EfficiencyRank, Is.Not.Null);
            Assert.That(player.PlayerStats.EfficiencyValue, Is.Not.Null);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayerHistory_ShouldReturnHistory()
        {
            var player = await rankingApi.GetPlayerHistory(EdGiardinaPlayerId);

            Assert.That(player.Player.PlayerId == EdGiardinaPlayerId);
            Assert.That(player.RankHistory.Count, Is.Positive);
            Assert.That(player.RatingHistory.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayerVersusPlayer_ShouldReturnPVP()
        {
            var player = await rankingApi.GetPlayerVersusPlayer(EdGiardinaPlayerId);

            Assert.That(player.PlayerId == EdGiardinaPlayerId);
            Assert.That(player.PlayerVersusPlayerRecords.Count, Is.Positive);
            Assert.That(player.TotalCompetitors, Is.Positive);
            Assert.That(player.TotalCompetitors, Is.EqualTo(player.PlayerVersusPlayerRecords.Count));
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayerVersusPlayerComparison_ShouldReturnPVP()
        {
            int playerTwoId = 36481;
            var player = await rankingApi.GetPlayerVersusPlayerComparison(EdGiardinaPlayerId, playerTwoId);

            Assert.That(player.PlayerOne.PlayerId == EdGiardinaPlayerId);
            Assert.That(player.PlayerTwo.PlayerId == playerTwoId);
            Assert.That(player.ComparisonRecords.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_NacsStandings_ShouldReturnStandings()
        {            
            var player = await rankingApi.GetNacsStandings();

            Assert.That(player.Count, Is.Positive);

            //OH SHIT IF THIS FAILS ED SUCKS AT PINBALL
            Assert.That(player.Single(n => n.StateProvinceName == "Rhode Island").CurrentLeaderPlayerKey, Is.EqualTo(EdGiardinaPlayerId));
        }

        [Test]
        public async Task PinballRankingApiV2_NacsStatistics_ShouldReturnStatistics()
        {
            var player = await rankingApi.GetNacsStatistics();

            Assert.That(player.StateProvinceCount, Is.Positive);
            Assert.That(player.CanadaPlayerCount, Is.Positive);
            Assert.That(player.NationalsPrizeValue, Is.Positive);
            Assert.That(player.TotalPlayerCount, Is.Positive);
            Assert.That(player.USAPlayerCount, Is.Positive);
            Assert.That(player.CanadaPlayerCount, Is.Positive);
            Assert.That(player.Year, Is.EqualTo(DateTime.Now.Year));
        }

    }
}
