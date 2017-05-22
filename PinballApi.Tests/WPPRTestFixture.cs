using NUnit.Framework;
using System.Configuration;
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

    }
}
