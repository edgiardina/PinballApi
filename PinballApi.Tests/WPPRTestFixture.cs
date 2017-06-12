using NUnit.Framework;
using PinballApi.Models.WPPR.Players;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    [TestFixture]
    class WPPRTestFixture
    {
        private PinballRankingApi rankingApi;

        [SetUp]
        public void SetUp()
        {
            var apiKey = ConfigurationSettings.AppSettings["WPPRKey"];
            rankingApi = new PinballRankingApi(apiKey);
        }

        [Test]
        public async Task Wppr_GetPlayer_ShouldReturnCorrectPlayer()
        {
            int playerId = 16927;
            var player = await rankingApi.GetPlayerRecord(playerId);

            Assert.That(player.Player.FirstName == "Ed");
        }

        [Test]
        public async Task Wppr_GetPlayer_ShouldRespectInvalidPlayerId()
        {
            int playerId = 999999;
            var player = await rankingApi.GetPlayerRecord(playerId);

            Assert.That(player == null);
        }

        [Test]
        public async Task Wppr_GetPlayer_ShouldHandleSuppressedPlayer()
        {
            PlayerRecord player = null;

            //Bowen Kerins is self-suppressed
            int playerId = 2;
            Assert.DoesNotThrow(async () => { player = await rankingApi.GetPlayerRecord(playerId); });

            Assert.That(player.Player.FirstName == "Suppresed");
        }

        [Test]
        public async Task Wppr_SearchPlayer_ByName()
        {
            var name = "J Sharpe";
            var search = await rankingApi.SearchForPlayerByName(name);

            Assert.That(search.Search.First().FirstName == "Josh");
            Assert.That(search.Search.First().PlayerId == 4);
        }

        [Test]
        public async Task Wppr_SearchPlayer_ByEmail()
        {
            var email = "ed@edgiardina.com";
            var search = await rankingApi.SearchForPlayerByEmail(email);

            Assert.That(search.Search.Single().FirstName == "Ed");
        }

    }
}
