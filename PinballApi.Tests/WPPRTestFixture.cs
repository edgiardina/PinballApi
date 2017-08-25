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
        public async Task Wppr_GetPlayerComparisons()
        {
            int playerId = 16927;
            var comparisons = await rankingApi.GetPlayerComparisons(playerId);

            Assert.That(comparisons.TotalCompetitors, Is.GreaterThan(0));
            Assert.That(comparisons.TotalCompetitors, Is.EqualTo(comparisons.PlayerVersusRecord.Count()));
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

        [Test]
        public async Task Wppr_GetCountryDirectors()
        {
            var search = await rankingApi.GetCountryDirectors();

            Assert.That(search.Search.First().CountryName == "Australia");
        }

        [Test]
        public async Task Wppr_GetPlayer_ShouldReturnPlayerHistory()
        {
            int playerId = 16927;
            var player = await rankingApi.GetPlayerHistory(playerId);

            Assert.That(player.Player.FirstName == "Ed");
            Assert.That(player.RankHistory.Count > 0);
            Assert.That(player.RatingHistory.Count > 0);
        }

        [Test]
        public async Task Wppr_GetTournament_ShouldReturnTournament()
        {
            int tournamentId = 18411;
            var tournament = await rankingApi.GetTournament(tournamentId);

            Assert.That(tournament.TournamentName, Is.EqualTo("PAPA World Pinball Championships"));
        }
        
        [Test]
        public async Task Wppr_GetTournamentResults_ShouldReturnResults()
        {
            int tournamentId = 18411;
            var tournamentResults = await rankingApi.GetTournamentResults(tournamentId);

            Assert.That(tournamentResults.TournamentName, Is.EqualTo("PAPA World Pinball Championships"));
        }

        [Test]
        public async Task Wppr_GetTournamentList_ShouldReturnList()
        {
            var tournamentList = await rankingApi.GetTournamentList();

            Assert.AreEqual(tournamentList.Tournament.Count, 50);
        }

        [Test]
        public async Task Wppr_SearchForTournament_ShouldReturnTournament()
        {
            var searchStr = "PAPA";
            var tournamentSearch = await rankingApi.TournamentSearch(searchStr);

            Assert.That(tournamentSearch.Search, Is.EqualTo(searchStr));
        }

    }
}
