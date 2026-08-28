using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PinballApi.Interfaces;
using PinballApi.Models.MatchPlay;
using PinballApi.Models.MatchPlay.Tournaments;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    /// <summary>
    /// Proves that each optional query parameter actually reaches the API and changes the result.
    /// </summary>
    /// <remarks>
    /// A parameter the wrapper drops returns exactly the same data as one it never sent, which is
    /// how the SetQueryParams bug hid until 4.0.0. Every test here therefore compares a filtered
    /// call against an unfiltered one instead of only checking that the call succeeds.
    /// </remarks>
    [TestFixture]
    internal class MatchPlayQueryParameterTestFixture
    {
        private const int BestGameTournamentId = 97100;      // 114 single player games
        private const int CardTournamentId = 95537;          // 53 cards, has a playoffs tournament
        private const int PlayoffsTournamentId = 100184;     // the playoffs child of 95537
        private const int GamesTournamentId = 97597;         // 28 multi-player games
        private const int SeriesWithGamesId = 2216;
        private const int CompletedTournamentId = 100085;

        private const int SpgPlayerId = 247261;              // 6 of the 114 games
        private const int SpgArenaId = 106413;               // 19 of the 114 games
        private const int CardPlayerId = 259119;             // 3 of the 53 cards

        private IMatchPlayApi matchPlayApi;

        [SetUp]
        public void SetUp()
        {
            var t = new ConfigurationBuilder().AddUserSecrets<Settings>().Build();

            matchPlayApi = new MatchPlayApi(t["MatchPlayApiToken"], rateLimitRetryCount: 2);
        }

        #region GetGames

        [Test]
        public async Task GetGames_GameIdsFilter_ShouldNarrowTheResult()
        {
            var all = await matchPlayApi.GetGames(new List<int> { GamesTournamentId });

            Assert.That(all.Count, Is.GreaterThan(2), "the tournament must have enough games to narrow");

            var wanted = all.Take(2).Select(g => g.GameId).ToList();

            var filtered = await matchPlayApi.GetGames(new List<int> { GamesTournamentId }, gameIds: wanted);

            Assert.That(filtered.Select(g => g.GameId), Is.EquivalentTo(wanted));
        }

        [Test]
        public async Task GetGames_SeriesIds_ShouldReturnGamesWithoutATournamentId()
        {
            var games = await matchPlayApi.GetGames(seriesIds: new List<int> { SeriesWithGamesId });

            Assert.That(games, Is.Not.Empty, "the series filter must reach the API");
        }

        [Test]
        public async Task GetGames_Page_ShouldReachTheApi()
        {
            var first = await matchPlayApi.GetGames(new List<int> { GamesTournamentId }, page: 1);
            var far = await matchPlayApi.GetGames(new List<int> { GamesTournamentId }, page: 99);

            Assert.That(first, Is.Not.Empty);
            Assert.That(far, Is.Empty, "a page past the end must come back empty, not repeat page one");
        }

        #endregion

        #region GetSinglePlayerGames

        [Test]
        public async Task GetSinglePlayerGames_PlayerFilter_ShouldNarrowTheResult()
        {
            var all = await matchPlayApi.GetSinglePlayerGames(BestGameTournamentId, limit: 500);
            var filtered = await matchPlayApi.GetSinglePlayerGames(BestGameTournamentId, limit: 500, playerId: SpgPlayerId);

            Assert.That(filtered, Is.Not.Empty);
            Assert.That(filtered.Count, Is.LessThan(all.Count), "the player filter must reach the API");
        }

        [Test]
        public async Task GetSinglePlayerGames_ArenaFilter_ShouldNarrowTheResult()
        {
            var all = await matchPlayApi.GetSinglePlayerGames(BestGameTournamentId, limit: 500);
            var filtered = await matchPlayApi.GetSinglePlayerGames(BestGameTournamentId, limit: 500, arenaId: SpgArenaId);

            Assert.That(filtered, Is.Not.Empty);
            Assert.That(filtered.Count, Is.LessThan(all.Count), "the arena filter must reach the API");
        }

        [Test]
        public async Task GetSinglePlayerGames_GameIdsFilter_ShouldNarrowTheResult()
        {
            var all = await matchPlayApi.GetSinglePlayerGames(BestGameTournamentId, limit: 500);
            var wanted = all.Take(2).Select(g => g.SinglePlayerGameId).ToList();

            var filtered = await matchPlayApi.GetSinglePlayerGames(BestGameTournamentId, limit: 500, gameIds: wanted);

            Assert.That(filtered.Select(g => g.SinglePlayerGameId), Is.EquivalentTo(wanted));
        }

        [Test]
        public async Task GetSinglePlayerGames_StatusFilter_ShouldReachTheApi()
        {
            var completed = await matchPlayApi.GetSinglePlayerGames(BestGameTournamentId, limit: 500,
                                                                    status: SinglePlayerGameStatus.Completed);
            var pending = await matchPlayApi.GetSinglePlayerGames(BestGameTournamentId, limit: 500,
                                                                  status: SinglePlayerGameStatus.Pending);

            Assert.That(completed, Is.Not.Empty);
            Assert.That(pending, Is.Empty, "this tournament is finished, so nothing is pending");
        }

        [Test]
        public async Task GetSinglePlayerGames_BestGameFlag_ShouldBeReadByValueNotPresence()
        {
            // Every game in this tournament is a best game. If the API read the flag by presence,
            // bestGame: false would behave like true and return all of them.
            var yes = await matchPlayApi.GetSinglePlayerGames(BestGameTournamentId, limit: 500, bestGame: true);
            var no = await matchPlayApi.GetSinglePlayerGames(BestGameTournamentId, limit: 500, bestGame: false);

            Assert.That(yes, Is.Not.Empty);
            Assert.That(no, Is.Empty, "bestGame is read by value, so false must exclude every best game");
        }

        #endregion

        #region GetCards

        [Test]
        public async Task GetCards_Limit_ShouldCapTheResult()
        {
            var limited = await matchPlayApi.GetCards(CardTournamentId, limit: 5);

            Assert.That(limited, Has.Count.EqualTo(5), "the API default is 25, so 5 proves the limit landed");
        }

        [Test]
        public async Task GetCards_PlayerFilter_ShouldNarrowTheResult()
        {
            var all = await matchPlayApi.GetCards(CardTournamentId, limit: 500);
            var filtered = await matchPlayApi.GetCards(CardTournamentId, limit: 500, playerId: CardPlayerId);

            Assert.That(filtered, Is.Not.Empty);
            Assert.That(filtered.Count, Is.LessThan(all.Count), "the player filter must reach the API");
        }

        [Test]
        public async Task GetCards_StatusFilter_ShouldReachTheApi()
        {
            var completed = await matchPlayApi.GetCards(CardTournamentId, limit: 500, status: SinglePlayerGameStatus.Completed);
            var pending = await matchPlayApi.GetCards(CardTournamentId, limit: 500, status: SinglePlayerGameStatus.Pending);

            Assert.That(completed, Is.Not.Empty);
            Assert.That(pending, Is.Empty, "this tournament is finished, so nothing is pending");
        }

        #endregion

        #region GetLocations

        [Test]
        public async Task GetLocations_IdFilter_ShouldNarrowTheResult()
        {
            var all = await matchPlayApi.GetLocations();

            Assert.That(all.Count, Is.GreaterThan(1), "there must be enough locations to narrow");

            var wanted = all.Take(1).Select(l => l.LocationId).ToList();

            var filtered = await matchPlayApi.GetLocations(locationIds: wanted);

            Assert.That(filtered.Select(l => l.LocationId), Is.EquivalentTo(wanted), "the locations filter must reach the API");
        }

        #endregion

        #region GetTournaments & GetTournament

        [Test]
        public async Task GetTournaments_Limit_ShouldCapTheResult()
        {
            var limited = await matchPlayApi.GetTournaments(limit: 3);

            Assert.That(limited, Has.Count.EqualTo(3), "the API default is 25, so 3 proves the limit landed");
        }

        [Test]
        public async Task GetTournament_WithoutIncludes_ShouldOmitTheRelatedObjects()
        {
            var tournament = await matchPlayApi.GetTournament(CompletedTournamentId);

            Assert.Multiple(() =>
            {
                Assert.That(tournament.Players, Is.Null, "the include flags are read by presence, so none may be sent");
                Assert.That(tournament.Arenas, Is.Null);
            });
        }

        [Test]
        public async Task GetTournament_WithIncludePlayers_ShouldReturnPlayers()
        {
            var tournament = await matchPlayApi.GetTournament(CompletedTournamentId, includePlayers: true);

            Assert.That(tournament.Players, Is.Not.Empty);
            Assert.That(tournament.Arenas, Is.Null, "asking for players must not drag in the arenas");
        }

        [Test]
        public async Task GetTournament_WithIncludeParent_ShouldReturnTheParent()
        {
            var tournament = await matchPlayApi.GetTournament(PlayoffsTournamentId, includeParent: true);

            Assert.That(tournament.ParentTournament, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(tournament.ParentTournament.TournamentId, Is.EqualTo(CardTournamentId));
                Assert.That(tournament.ParentTournament.Status, Is.EqualTo(TournamentStatus.Completed));
            });
        }

        [Test]
        public async Task GetTournament_WithIncludePlayoffs_ShouldReturnThePlayoffs()
        {
            var tournament = await matchPlayApi.GetTournament(CardTournamentId, includePlayoffs: true);

            Assert.That(tournament.PlayoffsTournament, Is.Not.Null);
            Assert.That(tournament.PlayoffsTournament.TournamentId, Is.EqualTo(PlayoffsTournamentId));
        }

        [Test]
        public async Task GetTournament_WithIncludeLinkedTournaments_ShouldReturnTheCollection()
        {
            var tournament = await matchPlayApi.GetTournament(CompletedTournamentId, includeLinkedTournaments: true);

            Assert.That(tournament.LinkedTournaments, Is.Not.Null,
                "the API returns the collection, so the model must surface it even when it is empty");
        }

        #endregion
    }
}
