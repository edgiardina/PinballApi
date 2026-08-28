using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PinballApi.Interfaces;
using PinballApi.Models.MatchPlay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    /// <summary>
    /// The cross-cutting parts of the client: cancellation, auto-paging and error reporting.
    /// </summary>
    [TestFixture]
    internal class MatchPlayApiSurfaceTestFixture
    {
        private const int CompletedTournamentId = 100085;
        private const int OwnedUserId = 3259;

        private IMatchPlayApi matchPlayApi;

        [SetUp]
        public void SetUp()
        {
            var t = new ConfigurationBuilder().AddUserSecrets<Settings>().Build();

            matchPlayApi = new MatchPlayApi(t["MatchPlayApiToken"], rateLimitRetryCount: 2);
        }

        #region cancellation

        [Test]
        public void MatchPlayApi_WithACancelledToken_ShouldNotCallTheApi()
        {
            using (var source = new CancellationTokenSource())
            {
                source.Cancel();

                Assert.ThrowsAsync<OperationCanceledException>(() => matchPlayApi.GetTournament(CompletedTournamentId, cancellationToken: source.Token));
            }
        }

        [Test]
        public void MatchPlayApi_Enumerate_WithACancelledToken_ShouldStop()
        {
            using (var source = new CancellationTokenSource())
            {
                source.Cancel();

                Assert.ThrowsAsync<OperationCanceledException>(async () =>
                {
                    await foreach (var _ in matchPlayApi.EnumerateTournaments(ownerUserId: OwnedUserId, cancellationToken: source.Token))
                    {
                        Assert.Fail("the enumerator should stop before it fetches a page");
                    }
                });
            }
        }

        #endregion

        #region auto-paging

        [Test]
        public async Task MatchPlayApi_EnumerateTournaments_ShouldCrossPageBoundaries()
        {
            var seen = new List<int>();

            // A limit of 2 forces several pages for an owner with more than two tournaments.
            await foreach (var tournament in matchPlayApi.EnumerateTournaments(ownerUserId: OwnedUserId, limit: 2))
            {
                seen.Add(tournament.TournamentId);

                if (seen.Count >= 7)
                {
                    break;
                }
            }

            Assert.That(seen, Has.Count.GreaterThan(2), "the enumerator must walk past the first page");
            Assert.That(seen, Is.Unique, "paging must not repeat a record");
        }

        [Test]
        public async Task MatchPlayApi_EnumerateTournamentArenaSummary_ShouldMatchTheSinglePageCall()
        {
            var firstPage = await matchPlayApi.GetTournamentArenaSummary(CompletedTournamentId);

            var enumerated = new List<int>();

            await foreach (var entry in matchPlayApi.EnumerateTournamentArenaSummary(CompletedTournamentId))
            {
                enumerated.Add(entry.ArenaId);
            }

            Assert.That(enumerated, Is.Not.Empty);
            Assert.That(enumerated, Is.Unique);
            Assert.That(enumerated.Count, Is.GreaterThanOrEqualTo(firstPage.Count),
                "walking every page cannot return fewer records than page one");
        }

        [Test]
        public async Task MatchPlayApi_EnumeratePlayers_ShouldRespectAnIdFilter()
        {
            var players = new List<Player>();

            await foreach (var player in matchPlayApi.EnumeratePlayers(players: new List<int> { 46132, 46133 }))
            {
                players.Add(player);
            }

            Assert.That(players, Has.Count.EqualTo(2));
        }

        #endregion

        #region error reporting

        [Test]
        public void MatchPlayApi_ForAMissingRecord_ShouldRaisePinballApiException()
        {
            var ex = Assert.ThrowsAsync<PinballApiException>(() => matchPlayApi.GetTournament(999999999));

            Assert.Multiple(() =>
            {
                Assert.That(ex.IsNotFound, Is.True, "a missing tournament should report as not found");
                Assert.That(ex.StatusCode, Is.Not.Null);
                Assert.That(ex.RequestUrl, Does.Contain("tournaments"));
                Assert.That(ex.InnerException, Is.Not.Null, "the Flurl exception should stay reachable");
            });
        }

        [Test]
        public void MatchPlayApi_WithABadToken_ShouldReportUnauthorized()
        {
            var api = new MatchPlayApi("not-a-real-token");

            var ex = Assert.ThrowsAsync<PinballApiException>(() => api.GetMyProfile());

            Assert.That(ex.IsUnauthorized, Is.True);
        }

        #endregion
    }
}
