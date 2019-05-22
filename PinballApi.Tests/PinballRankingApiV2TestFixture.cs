using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using PinballApi.Models.WPPR.v2.Players;
using PinballApi.Models.WPPR.v2.Calendar;
using System;
using System.Linq;
using System.Threading.Tasks;
using PinballApi.Models.WPPR.v2;
using PinballApi.Models.WPPR.v2.Rankings;
using PinballApi.Models.WPPR.v2.Tournaments;

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
            Assert.That(player.Gender, Is.EqualTo(Gender.Male));
            //To make sure unranked player changes didn't ruin ranked player efficiency stats
            Assert.That(player.PlayerStats.EfficiencyRank, Is.Not.Null);
            Assert.That(player.PlayerStats.EfficiencyValue, Is.Not.Null);
            Assert.That(player.PlayerStats.HighestRankDate.HasValue, Is.True);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayer_ShouldReturnPlayerWithEmptyGender()
        {
            var playerId = 61313;
            var player = await rankingApi.GetPlayer(playerId);

            Assert.That(player.Gender, Is.Null);
        }

        /*
         {"player":[{"player_id":"72827","first_name":"Dan","last_name":"Tyler ","initials":"","gender":"male","age":"","excluded_flag":"false","city":"","stateprov":"","country_name":"United States","country_code":"US","ifpa_registered":"false","profile_photo":"","player_stats":{"current_wppr_rank":"21526","last_month_rank":"0","last_year_rank":"0","highest_rank":"0","highest_rank_date":"0000-00-00","current_wppr_value":"0.33","wppr_points_all_time":"0.33","best_finish":"24","best_finish_count":"1","average_finish":"24","average_finish_last_year":"0","total_events_all_time":"1","total_active_events":"1","total_events_away":"0","ratings_rank":"10616","ratings_value":"1255.97","efficiency_rank":"Not Ranked","efficiency_value":""},"championshipSeries_us":[{"group_code":"PA","group_name":"Pennsylvania","series_rank":"515","series_name":null,"year":"2019"}]}]}
             */
        [Test]
        public async Task PinballRankingApiV2_GetPlayer_ShouldReturnPlayerWithBadHighestRankDate()
        {
            var playerId = 72827;
            var player = await rankingApi.GetPlayer(playerId);

            Assert.That(player.PlayerStats.HighestRankDate.HasValue, Is.False);
        }

        [Test]
        [Ignore("endpoint appears busted for now")]
        public async Task PinballRankingApiV2_GetPlayers_ShouldReturnCorrectPlayers()
        {
            var players = await rankingApi.GetPlayers(new System.Collections.Generic.List<int> { EdGiardinaPlayerId, 2 });

            Assert.That(players.First().FirstName == "Ed");
            Assert.That(players.Last().FirstName == "Bowen");
            //To make sure unranked player changes didn't ruin ranked player efficiency stats
            Assert.That(players.First().PlayerStats.EfficiencyRank, Is.Not.Null);
            Assert.That(players.First().PlayerStats.EfficiencyValue, Is.Not.Null);
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
        public async Task PinballRankingApiV2_GetPlayerBySearch_ShouldReturnPlayer()
        {
            var searchFilter = new PlayerSearchFilter { Name = "Giardina" };
            var player = await rankingApi.GetPlayersBySearch(searchFilter);

            Assert.That(player.Results.Any(n => n.PlayerId == EdGiardinaPlayerId), Is.True);
        }

        [Test]
        [TestCase(RankingType.Main, ResultType.Active)]
        [TestCase(RankingType.Main, ResultType.Inactive)]
        [TestCase(RankingType.Main, ResultType.NonActive)]
        public async Task PinballRankingApiV2_GetPlayerResults_ShouldReturnResults(RankingType rankingType, ResultType resultType)
        {
            var playerResults = await rankingApi.GetPlayerResults(EdGiardinaPlayerId, rankingType, resultType);

            Assert.That(playerResults.ResultsCount, Is.Positive);
            Assert.That(playerResults.RankingType, Is.EqualTo(rankingType));
            Assert.That(playerResults.ResultsType, Is.EqualTo(resultType));
        }

        [Test]
        public async Task PinballRankingApiV2_NacsStandings_ShouldReturnStandings()
        {
            var player = await rankingApi.GetNacsStandings();

            Assert.That(player.Count, Is.Positive);

            //OH SHIT IF THIS FAILS ED SUCKS AT PINBALL
            Assert.That(player.Single(n => n.StateProvinceName == "Rhode Island").CurrentLeaderPlayerId, Is.EqualTo(EdGiardinaPlayerId));
        }

        [Test]
        public async Task PinballRankingApiV2_NacsStateProvinceStandings_ShouldReturnStandings()
        {
            string stateAbbrv = "RI";

            var player = await rankingApi.GetNacsStateProvinceStandings(stateAbbrv);

            Assert.That(player.PlayerStandings.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_NacsPastWinners_ShouldReturnWinners()
        {
            string stateAbbrv = "RI";

            var winners = await rankingApi.GetNacsPastWinners();

            Assert.That(winners.Count, Is.Positive);
            Assert.That(winners.Single(n => n.StateProvince == "Rhode Island").Winners.Any(n => n.PlayerId == EdGiardinaPlayerId), Is.True);
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


        [Test]
        public async Task PinballRankingApiV2_GetCalendarEntry_ShouldReturnEntry()
        {
            var tourneyid = 29978;

            var calendarEntry = await rankingApi.GetCalendarEntry(tourneyid);

            Assert.That(calendarEntry.TournamentName, Is.EqualTo("PinCrossing Monthly Tournament"));
        }

        [Test]
        public async Task PinballRankingApiV2_GetCalendarEntryBySearch_ShouldReturnEntry()
        {
            var searchFilter = new CalendarSearchFilter { StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(2) };

            var calendarEntries = await rankingApi.GetCalendarEntriesBySearch(searchFilter);

            Assert.That(calendarEntries.CalendarEntries.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetCalendarEntryByDistance_ShouldReturnEntry()
        {
            var searchFilter = new CalendarDistanceSearchFilter { Address = "Providence, RI", Distance = 50, DistanceType = DistanceType.Miles };

            var calendarEntries = await rankingApi.GetCalendarEntriesByDistance(searchFilter);

            Assert.That(calendarEntries.CalendarEntries.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetRankingCountries_ShouldReturnCountries()
        {
            var countries = await rankingApi.GetRankingCountries();

            Assert.That(countries.TotalCount, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetRankingCountry_ShouldReturnRanking()
        {
            var ranking = await rankingApi.GetRankingForCountry("United States");

            Assert.That(ranking.TotalCount, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetRankingElite_ShouldReturnRankingElite()
        {
            var ranking = await rankingApi.GetEliteRanking();

            Assert.That(ranking.TotalCount, Is.Positive);
        }


        [Test]
        public async Task PinballRankingApiV2_GetRankingElitePvp_ShouldReturnRankingElite()
        {
            var elitePlayerId = 1256;
            var ranking = await rankingApi.GetElitePlayerVersusPlayer(elitePlayerId);

            Assert.That(ranking.Records.Count, Is.Positive);
        }

        [Test]
        [TestCase(TournamentType.Open)]
        [TestCase(TournamentType.Women)]
        public async Task PinballRankingApiV2_GetRankingWomen_ShouldReturnRanking(TournamentType tournamentType)
        {
            var ranking = await rankingApi.GetRankingForWomen(tournamentType);

            Assert.That(ranking.ReturnCount, Is.Positive);
            Assert.That(ranking.TournamentType, Is.EqualTo(tournamentType));
        }

        [Test]
        public async Task PinballRankingApiV2_GetRankingYouth_ShouldReturnRanking()
        {
            var ranking = await rankingApi.GetRankingForYouth(1, 100);

            Assert.That(ranking.ReturnCount, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetWpprRanking_ShouldReturnRanking()
        {
            var ranking = await rankingApi.GetWpprRanking();

            Assert.That(ranking.ReturnCount, Is.Positive);
        }


        [Test]
        public async Task PinballRankingApiV2_GetRankingCustom_ShouldReturnCustom()
        {
            int viewId = 13;
            var customView = await rankingApi.GetRankingCustomView(viewId);

            Assert.That(customView.TotalCount, Is.Positive);
            Assert.That(customView.ViewId, Is.EqualTo(viewId));
        }

        [Test]
        public async Task PinballRankingApiV2_GetRankingCustomList_ShouldReturnCustomList()
        {
            var countries = await rankingApi.GetRankingCustomViewList();

            Assert.That(countries.TotalCount, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetTournament_ShouldReturnTournament()
        {
            var tournamentId = 25586;

            var tournament = await rankingApi.GetTournament(tournamentId);

            Assert.That(tournament.TournamentId, Is.EqualTo(tournamentId));
        }

        [Test]
        public async Task PinballRankingApiV2_GetTournamentResults_ShouldReturnTournamentResults()
        {
            var tournamentId = 25586;

            var tournament = await rankingApi.GetTournamentResults(tournamentId);

            Assert.That(tournament.Results.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetTournamentResults_WithUnratedPlayerShouldReturnTournamentResults()
        {
            var tournamentId = 21076;

            var tournament = await rankingApi.GetTournamentResults(tournamentId);

            Assert.That(tournament.Results.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetTournamentBySearch_ShouldReturnTournament()
        {
            var searchCriteria = new TournamentSearchFilter { Name = "Pinburgh" };

            var tournament = await rankingApi.GetTournamentBySearch(searchCriteria);

            Assert.That(tournament.Results.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetActiveLeagues_ShouldReturnLeague()
        {
            var tournament = await rankingApi.GetActiveLeagues();

            Assert.That(tournament.TotalEntries, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetHistoricalLeagues_ShouldReturnLeague()
        {
            var tournament = await rankingApi.GetHistoricalLeagues();

            Assert.That(tournament.TotalEntries, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetNacsDirectors_ShouldReturnDirectors()
        {          
            var directors = await rankingApi.GetNacsDirectors();

            Assert.That(directors.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetCountryDirectors_ShouldReturnDirectors()
        {
            var directors = await rankingApi.GetCountryDirectors();

            Assert.That(directors.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetOverallStatistics_ShouldReturnStats()
        {
            var stats = await rankingApi.GetOverallStatistics();

            Assert.That(stats.TournamentCount, Is.Positive);
            Assert.That(stats.OverallPlayerCount, Is.Positive);
            Assert.That(stats.TournamentCountLastMonth, Is.Positive);
            Assert.That(stats.TournamentCountThisYear, Is.Positive);
            Assert.That(stats.TournamentPlayerCount, Is.Positive);
            Assert.That(stats.TournamentPlayerCountAverage, Is.Positive);           
        }

        [Test]
        public async Task PinballRankingApiV2_GetEventsByYearStatistics()
        {
            var stat = await rankingApi.GetEventsByYearStatistics();

            var firstStat = stat.First();

            Assert.That(stat.Count, Is.Positive);
            Assert.That(firstStat.CountryCount, Is.Positive);
            Assert.That(firstStat.Year, Is.Positive);
            Assert.That(firstStat.StatsRank, Is.Positive);
            Assert.That(firstStat.PlayerCount, Is.Positive);
            Assert.That(firstStat.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetLargestTournamentStatistics()
        {
            var stat = await rankingApi.GetLargestTournamentStatistics();

            var firstStat = stat.First();

            Assert.That(stat.Count, Is.Positive);
            Assert.That(firstStat.StatsRank, Is.Positive);
            Assert.That(firstStat.PlayerCount, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetLucrativeTournamentStatistics()
        {
            var stat = await rankingApi.GetLucrativeTournamentStatistics();

            var firstStat = stat.First();

            Assert.That(stat.Count, Is.Positive);
            Assert.That(firstStat.StatsRank, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayersByYearStatistics()
        {
            var stat = await rankingApi.GetPlayersByYearStatistics();

            var firstStat = stat.First();

            Assert.That(stat.Count, Is.Positive);
            Assert.That(firstStat.StatsRank, Is.Positive);
            Assert.That(firstStat.Count, Is.Positive);
            Assert.That(firstStat.Previous2_YearCount, Is.Positive);
            Assert.That(firstStat.PreviousYearCount, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayersByStateStatistics()
        {
            var stat = await rankingApi.GetPlayersByStateStatistics();

            var firstStat = stat.First();

            Assert.That(stat.Count, Is.Positive);
            Assert.That(firstStat.StatsRank, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetTournamentsByStateStatistics()
        {
            var stat = await rankingApi.GetTournamentsByStateStatistics();

            var firstStat = stat.First();

            Assert.That(stat.Count, Is.Positive);
            Assert.That(firstStat.StatsRank, Is.Positive);
            Assert.That(firstStat.TotalPointsAll, Is.Positive);
            Assert.That(firstStat.TotalPointsTourValue, Is.Positive);
        }

    }
}
