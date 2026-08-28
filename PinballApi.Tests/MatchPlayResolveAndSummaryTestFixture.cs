using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PinballApi.Interfaces;
using PinballApi.Models.MatchPlay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    /// <summary>
    /// The resolve-unknown and summary endpoints, and the id filters that go with them.
    /// </summary>
    [TestFixture]
    internal class MatchPlayResolveAndSummaryTestFixture
    {
        // A completed tournament. MatchPlay builds the summary endpoints only after a
        // tournament closes.
        private const int CompletedTournamentId = 100085;
        private const int JoeWadePlayerId = 99812;
        private const int JimTowePlayerId = 100882;
        private const int WhirlwindArenaId = 41176;
        private const int DeadpoolArenaId = 60324;
        private const int JoeWadeUserId = 2697;

        // /api/players and /api/arenas are scoped to the organizer who owns the API token, so the
        // id filter tests must use ids that belong to that organizer.
        private const int OwnedPlayerIdOne = 46132;
        private const int OwnedPlayerIdTwo = 46133;
        private const int OwnedArenaIdOne = 20174;
        private const int OwnedArenaIdTwo = 20175;

        private IMatchPlayApi matchPlayApi;

        [SetUp]
        public void SetUp()
        {
            var t = new ConfigurationBuilder().AddUserSecrets<Settings>().Build();

            matchPlayApi = new MatchPlayApi(t["MatchPlayApiToken"]);
        }

        #region resolve unknown

        [Test]
        public async Task MatchPlayApi_ResolveUnknownPlayers_ShouldReturnPlayers()
        {
            var players = await matchPlayApi.ResolveUnknownPlayers(new List<int> { JoeWadePlayerId, JimTowePlayerId });

            Assert.That(players, Has.Count.EqualTo(2));
            Assert.That(players.Select(p => p.PlayerId), Is.EquivalentTo(new[] { JoeWadePlayerId, JimTowePlayerId }));
            Assert.That(players.All(p => !string.IsNullOrWhiteSpace(p.Name)), Is.True);
        }

        [Test]
        public async Task MatchPlayApi_ResolveUnknownArenas_ShouldReturnArenas()
        {
            var arenas = await matchPlayApi.ResolveUnknownArenas(new List<int> { WhirlwindArenaId, DeadpoolArenaId });

            Assert.That(arenas, Has.Count.EqualTo(2));
            Assert.That(arenas.Single(a => a.ArenaId == WhirlwindArenaId).Name, Is.EqualTo("Whirlwind"));
            Assert.That(arenas.All(a => !string.IsNullOrWhiteSpace(a.OpdbId)), Is.True);
        }

        [Test]
        public async Task MatchPlayApi_ResolveUnknownUsers_ShouldReturnUsers()
        {
            var users = await matchPlayApi.ResolveUnknownUsers(new List<int> { JoeWadeUserId });

            Assert.That(users, Has.Count.EqualTo(1));
            Assert.That(users[0].UserId, Is.EqualTo(JoeWadeUserId));
            Assert.That(users[0].Name, Is.Not.Empty);
        }

        [Test]
        public async Task MatchPlayApi_ResolveUnknownTournamentPlayers_ShouldIncludePivotData()
        {
            var players = await matchPlayApi.ResolveUnknownTournamentPlayers(CompletedTournamentId, new List<int> { JoeWadePlayerId });

            Assert.That(players, Has.Count.EqualTo(1));
            Assert.That(players[0].TournamentPlayer, Is.Not.Null, "the tournament variant should fill in the pivot data");
        }

        [Test]
        public async Task MatchPlayApi_ResolveUnknownTournamentArenas_ShouldIncludePivotData()
        {
            var arenas = await matchPlayApi.ResolveUnknownTournamentArenas(CompletedTournamentId, new List<int> { WhirlwindArenaId });

            Assert.That(arenas, Has.Count.EqualTo(1));
            Assert.That(arenas[0].TournamentArena, Is.Not.Null, "the tournament variant should fill in the pivot data");
        }

        [Test]
        public void MatchPlayApi_ResolveUnknownPlayers_ShouldRejectTooManyIds()
        {
            var tooMany = Enumerable.Range(1, MatchPlayApi.MaxResolveIds + 1).ToList();

            Assert.ThrowsAsync<ArgumentException>(() => matchPlayApi.ResolveUnknownPlayers(tooMany));
        }

        [Test]
        public void MatchPlayApi_ResolveUnknownPlayers_ShouldRejectEmptyList()
        {
            Assert.ThrowsAsync<ArgumentException>(() => matchPlayApi.ResolveUnknownPlayers(new List<int>()));
            Assert.ThrowsAsync<ArgumentException>(() => matchPlayApi.ResolveUnknownPlayers(null));
        }

        #endregion

        #region summaries

        [Test]
        public async Task MatchPlayApi_GetTournamentArenaSummary_ShouldReturnOneEntryPerArena()
        {
            var summary = await matchPlayApi.GetTournamentArenaSummary(CompletedTournamentId);

            Assert.That(summary, Is.Not.Empty);

            var entry = summary.First();

            Assert.Multiple(() =>
            {
                Assert.That(entry.TournamentId, Is.EqualTo(CompletedTournamentId));
                Assert.That(entry.TotalGames, Is.GreaterThan(0));
                Assert.That(entry.OpdbId, Is.Not.Empty);
                Assert.That(summary.Select(s => s.ArenaId), Is.Unique);
            });
        }

        [Test]
        public async Task MatchPlayApi_GetTournamentPlayerArenaSummary_ShouldReturnWinsAndLosses()
        {
            var summary = await matchPlayApi.GetTournamentPlayerArenaSummary(CompletedTournamentId);

            Assert.That(summary, Is.Not.Empty);
            Assert.That(summary.All(s => s.TournamentId == CompletedTournamentId), Is.True);
            Assert.That(summary.Any(s => s.Wins > 0), Is.True);
            Assert.That(summary.All(s => s.PlayerId > 0), Is.True);
        }

        [Test]
        public async Task MatchPlayApi_GetTournamentMatchSummary_ShouldReturnOpponents()
        {
            var summary = await matchPlayApi.GetTournamentMatchSummary(CompletedTournamentId);

            Assert.That(summary, Is.Not.Empty);
            Assert.That(summary.All(s => s.OpponentId > 0), Is.True);
            Assert.That(summary.All(s => s.PlayerId != s.OpponentId), Is.True);
        }

        #endregion

        #region id filters

        // These filters were silently dropped before 4.0.0. The wrapper called Flurl's
        // SetQueryParams(name, value) overload, which adds parameters WITHOUT values.

        [Test]
        public async Task MatchPlayApi_GetPlayers_ShouldApplyIdFilter()
        {
            var players = await matchPlayApi.GetPlayers(players: new List<int> { OwnedPlayerIdOne, OwnedPlayerIdTwo });

            Assert.That(players, Has.Count.EqualTo(2), "the players filter must reach the API");
            Assert.That(players.Select(p => p.PlayerId), Is.EquivalentTo(new[] { OwnedPlayerIdOne, OwnedPlayerIdTwo }));
        }

        [Test]
        public async Task MatchPlayApi_GetArenas_ShouldApplyIdFilter()
        {
            var arenas = await matchPlayApi.GetArenas(arenaIds: new List<string> { OwnedArenaIdOne.ToString(), OwnedArenaIdTwo.ToString() });

            Assert.That(arenas, Has.Count.EqualTo(2), "the arenas filter must reach the API");
            Assert.That(arenas.Select(a => a.ArenaId), Is.EquivalentTo(new[] { OwnedArenaIdOne, OwnedArenaIdTwo }));
        }

        #endregion

        #region games & cards paging

        [Test]
        public void MatchPlayApi_GetGames_ShouldRejectMissingTournamentAndSeries()
        {
            Assert.ThrowsAsync<ArgumentException>(() => matchPlayApi.GetGames());
        }

        [Test]
        public async Task MatchPlayApi_GetSinglePlayerGames_ShouldApplyLimit()
        {
            var games = await matchPlayApi.GetSinglePlayerGames(97100, limit: 5);

            Assert.That(games, Is.Not.Empty);
            Assert.That(games, Has.Count.LessThanOrEqualTo(5), "the limit must reach the API");
        }

        [Test]
        [Ignore("Returns 403 even for a tournament the token owns. The endpoint needs a scorekeeper scoped token.")]
        public async Task MatchPlayApi_GetQueues_ShouldReturnQueues()
        {
            var queues = await matchPlayApi.GetQueues(CompletedTournamentId);

            Assert.That(queues, Is.Not.Null, "a format without queues returns an empty list, not null");
        }

        #endregion
    }
}
