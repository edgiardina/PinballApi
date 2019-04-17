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
            int playerId = 16927;
            var player = await rankingApi.GetPlayerRecord(playerId);

            Assert.That(player.FirstName == "Ed");
            //To make sure unranked player changes didn't ruin ranked player efficiency stats
            Assert.That(player.PlayerStats.EfficiencyRank, Is.Not.Null);
            Assert.That(player.PlayerStats.EfficiencyValue, Is.Not.Null);
        }

    }
}
