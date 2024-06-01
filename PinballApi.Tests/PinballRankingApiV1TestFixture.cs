using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PinballApi.Models.WPPR.v1.Calendar;
using PinballApi.Models.WPPR.v1.Players;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PinballApi.Tests
{
    [TestFixture]
    class PinballRankingApiV1TestFixture
    {
        private PinballRankingApiV1 rankingApi;

        [SetUp]
        public void SetUp()
        {
            var t = new ConfigurationBuilder().AddUserSecrets<Settings>().Build();

            var apiKey = t["WPPRKey"];
            rankingApi = new PinballRankingApiV1(apiKey);
        }

        [Test]
        public async Task PinballRankingApiV1_GetPlayer_ShouldReturnCorrectPlayer()
        {
            int playerId = 16927;
            var player = await rankingApi.GetPlayerRecord(playerId);

            Assert.That(player.Player.FirstName == "Ed");
            //To make sure unranked player changes didn't ruin ranked player efficiency stats
            Assert.That(player.PlayerStats.EfficiencyRank, Is.Not.Null);
            Assert.That(player.PlayerStats.EfficiencyValue, Is.Not.Null);
        }

        [Test]
        public async Task PinballRankingApiV1_GetPlayer_ShouldRespectInvalidPlayerId()
        {
            int playerId = 999999;
            var player = await rankingApi.GetPlayerRecord(playerId);

            Assert.That(player == null);
        }

        [Test]
        [Ignore("Suppressed players not yet handled. TODO")]
        public async Task PinballRankingApiV1_GetPlayer_ShouldHandleSuppressedPlayer()
        {
            PlayerRecord player = null;

            //Bowen Kerins is self-suppressed
            int playerId = 2;
            Assert.DoesNotThrowAsync(async () => { player = await rankingApi.GetPlayerRecord(playerId); });

            Assert.That(player.Player.FirstName == "Suppresed");
        }

        [Test]
        public async Task PinballRankingApiV1_GetPlayerResults_ShouldReturnCorrectPlayer()
        {
            int playerId = 16927;
            var player = await rankingApi.GetPlayerResults(playerId);

            Assert.That(player.ResultsCount, Is.GreaterThan(0));
            Assert.That(player.PlayerId, Is.EqualTo(playerId));
        }

        [Test]
        public async Task PinballRankingApiV1_GetPlayer_ShouldHandleUnrankedPlayer()
        {
            PlayerRecord player = null;
            int playerId = 46546;
            Assert.DoesNotThrowAsync(async () => { player = await rankingApi.GetPlayerRecord(playerId); });

            Assert.That(player.Player.LastName == "Giardina");
            Assert.That(player.Player.IfpaRegistered, Is.False);
            Assert.That(player.PlayerStats.EfficiencyRank, Is.Null);
            Assert.That(player.PlayerStats.EfficiencyValue, Is.Null);
        }

        [Test]
        public async Task PinballRankingApiV1_GetPlayer_ShouldHandleUnregisteredPlayer()
        {
            PlayerRecord player = null;
            int playerId = 66660;
            Assert.DoesNotThrowAsync(async () => { player = await rankingApi.GetPlayerRecord(playerId); });

            Assert.That(player.Player.FirstName == "Kelly");
            Assert.That(player.Player.IfpaRegistered, Is.False);
        }

        [Test]
        public async Task PinballRankingApiV1_GetPlayerComparisons()
        {
            int playerId = 16927;
            var comparisons = await rankingApi.GetPlayerComparisons(playerId);

            Assert.That(comparisons.TotalCompetitors, Is.GreaterThan(0));
            Assert.That(comparisons.TotalCompetitors, Is.EqualTo(comparisons.Pvp.Count()));
        }

        [Test]
        public async Task PinballRankingApiV1_SearchPlayer_ByName()
        {
            var name = "J Sharpe";
            var search = await rankingApi.SearchForPlayerByName(name);

            Assert.That(search.Search.First().FirstName == "Josh");
            Assert.That(search.Search.First().PlayerId == 4);
        }

        [Test]
        public async Task PinballRankingApiV1_SearchPlayer_NameDoesNotExist()
        {
            var name = "definitelynotarealname";
            var search = await rankingApi.SearchForPlayerByName(name);

            Assert.That(search.Search.Count == 0);
        }

        [Test]
        public async Task PinballRankingApiV1_SearchPlayer_ByEmail()
        {
            var email = "ed@edgiardina.com";
            var search = await rankingApi.SearchForPlayerByEmail(email);

            Assert.That(search.Search.Single().FirstName == "Ed");
        }

        [Test]
        public async Task PinballRankingApiV1_SearchPlayer_EmailDoesNotExist()
        {
            var name = "definitelynotarealemail@notarealdomain.com";
            var search = await rankingApi.SearchForPlayerByName(name);

            Assert.That(search.Search.Count == 0);
        }

        [Test]
        public async Task PinballRankingApiV1_GetCountryDirectors()
        {
            var search = await rankingApi.GetCountryDirectors();

            Assert.That(search.Search.Count > 0);
            Assert.That(search.Search.Any(n => n.CountryName == "Australia"));
        }

        [Test]
        public async Task PinballRankingApiV1_GetPlayer_ShouldReturnPlayerHistory()
        {
            int playerId = 16927;
            var player = await rankingApi.GetPlayerHistory(playerId);

            Assert.That(player.Player.FirstName == "Ed");
            Assert.That(player.RankHistory.Count > 0);
            Assert.That(player.RatingHistory.Count > 0);
        }



        [Test]
        public async Task PinballRankingApiV1_GetTournament_ShouldReturnTournament()
        {
            int tournamentId = 18411;
            var tournament = await rankingApi.GetTournament(tournamentId);

            Assert.That(tournament.TournamentName, Is.EqualTo("PAPA World Pinball Championships"));
        }

        [Test]
        public async Task PinballRankingApiV1_GetTournamentResults_ShouldReturnResults()
        {
            int tournamentId = 18411;
            var tournamentResults = await rankingApi.GetTournamentResults(tournamentId);

            Assert.That(tournamentResults.TournamentName, Is.EqualTo("PAPA World Pinball Championships"));
        }

        [Test]
        public async Task PinballRankingApiV1_GetTournamentList_ShouldReturnList()
        {
            var tournamentList = await rankingApi.GetTournamentList(count: 10);

            Assert.That(tournamentList.Tournament.Count, Is.EqualTo(10));
        }

        [Test]
        public async Task PinballRankingApiV1_SearchForTournament_ShouldReturnTournament()
        {
            var searchStr = "PAPA";
            var tournamentSearch = await rankingApi.TournamentSearch(searchStr);

            Assert.That(tournamentSearch.Search, Is.EqualTo(searchStr));
        }

        [Test]
        public async Task PinballRankingApiV1_GetRankings_ShouldReturnRankings()
        {
            var totalrecords = 75;
            var startpos = 5;

            var rankings = await rankingApi.GetRankings(startpos, totalrecords);
            Assert.That(rankings.Rankings.Count, Is.EqualTo(totalrecords));
            Assert.That(rankings.Rankings.First().CurrentWpprRank, Is.EqualTo(startpos));
            Assert.That(rankings.RankCountryName, Is.Null);
        }


        [Test]
        public async Task PinballRankingApiV1_GetRankingsByCountry_ShouldReturnRankings()
        {
            var totalrecords = 75;
            var startpos = 5;
            var country = "Austria";

            var rankings = await rankingApi.GetRankings(startpos, totalrecords, countryName: country);
            Assert.That(rankings.Rankings.Count, Is.EqualTo(totalrecords));
            Assert.That(rankings.RankCountryName, Is.EqualTo(country));
        }

        [Test]
        public async Task Wpp_GetActiveCalendar_ShouldReturnActiveCalendar()
        {
            var country = "Sweden";
            var calendar = await rankingApi.GetActiveCalendar(country);

            Assert.That(calendar.Calendar.All(n => n.Country == country));
            Assert.That(calendar.Calendar.All(n => n.EndDate.Date >= DateTime.Now.Date));
        }

        [Test]
        [Ignore("IFPA data has a bug where 0000-00-01 is listed as a date in the calendar history")]
        public async Task Wpp_GetHistoryCalendar_ShouldReturnHistoryCalendar()
        {
            var country = "Switzerland";
            var calendar = await rankingApi.GetCalendarHistory(country);

            Assert.That(calendar.Calendar.All(n => n.Country == country));
            Assert.That(calendar.Calendar.All(n => n.EndDate < DateTime.Now));
        }

        [Test]
        public async Task Wpp_GetCalendarById_ShouldReturnCalendar()
        {
            var calendarId = 19551;
            var calendar = await rankingApi.GetCalendarById(calendarId);

            Assert.That(calendar.Calendar.First().CalendarId, Is.EqualTo(calendarId));
            Assert.That(calendar.Calendar.First().TournamentName, Is.EqualTo("The Racket Fortnightly"));
        }

        [Test]
        public async Task PinballRankingApiV1_GetCalendarSearch_ShouldReturnCalendar()
        {
            var address = "346 Rochambeau Ave, Providence, RI";
            var distance = 250;
            var units = DistanceUnit.Miles;

            var calendar = await rankingApi.GetCalendarSearch(address, distance, units);

            Assert.That(calendar.TotalEntries, Is.GreaterThan(0));
        }

        [Test]
        [Ignore("known not working, informed Brian S.")]
        public async Task PinballRankingApiV1_GetCalendarSearch_ForItaly_ShouldReturnCalendar()
        {
            var address = "Italy";
            var distance = 115;
            var units = DistanceUnit.Miles;

            var calendar = await rankingApi.GetCalendarSearch(address, distance, units);

            Assert.That(calendar.TotalEntries, Is.GreaterThan(0));
        }

        [Test]
        public async Task PinballRankingApiV1_GetPvp_ShouldReturnPvp()
        {
            var playerOneId = 16927;
            var playerTwoId = 19611;

            var pvp = await rankingApi.GetPvp(playerOneId, playerTwoId);

            Assert.That(pvp.Pvp.Count > 0);
        }

        [Test]
        public async Task PinballRankingApiV1_GetPointsThisYearStat()
        {
            var stat = await rankingApi.GetPointsThisYearStats();

            Assert.That(stat.Count, Is.GreaterThan(0));
        }

        [Test]
        [Ignore("Throwing SQL Error")]
        public async Task PinballRankingApiV1_GetMostEventsStat()
        {
            var stat = await rankingApi.GetMostEventsStats();

            Assert.That(stat.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task PinballRankingApiV1_GetPlayersByCountryStat()
        {
            var stat = await rankingApi.GetPlayersByCountryStat();

            Assert.That(stat.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task PinballRankingApiV1_GetEventsPerYearStat()
        {
            var stat = await rankingApi.GetEventsPerYearStat();

            Assert.That(stat.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task PinballRankingApiV1_GetPlayersPerYearStat()
        {
            var stat = await rankingApi.GetPlayersPerYearStat();

            Assert.That(stat.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task PinballRankingApiV1_GetBiggestMoversStat()
        {
            var stat = await rankingApi.GetBiggestMoversStat();

            Assert.That(stat.Count, Is.GreaterThan(0));
        }

    }
}
