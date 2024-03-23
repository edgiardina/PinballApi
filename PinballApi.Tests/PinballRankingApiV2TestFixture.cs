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
            var t = new ConfigurationBuilder().AddUserSecrets<Settings>().Build();

            var apiKey = t["WPPRKey"];
            rankingApi = new PinballRankingApiV2(apiKey);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayer_ShouldReturnCorrectPlayer()
        {
            var player = await rankingApi.GetPlayer(EdGiardinaPlayerId);

            Assert.That(player.FirstName == "Ed");
            //For some reason my player ID lost its gender
            //Assert.That(player.Gender, Is.EqualTo(Gender.Male));
            //To make sure unranked player changes didn't ruin ranked player efficiency stats
            Assert.That(player.PlayerStats.EfficiencyRank, Is.Not.Null);
            Assert.That(player.PlayerStats.EfficiencyValue, Is.Not.Null);
            Assert.That(player.PlayerStats.HighestRankDate.HasValue, Is.True);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayer_ShouldReturnPlayerWithWomensFlag()
        {
            var playerId = 61534;
            var player = await rankingApi.GetPlayer(playerId);

            Assert.That(player.WomensFlag, Is.True);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayer_ShouldReturnPlayerWithoutWomensFlag()
        {
            var playerId = 16927;
            var player = await rankingApi.GetPlayer(playerId);

            Assert.That(player.WomensFlag, Is.False);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayers_ShouldReturnCorrectPlayers()
        {
            var players = await rankingApi.GetPlayers(new System.Collections.Generic.List<int> { EdGiardinaPlayerId, 2 });

            Assert.That(players.Last().FirstName == "Ed");
            Assert.That(players.First().FirstName == "Bowen");
            //To make sure unranked player changes didn't ruin ranked player efficiency stats
            Assert.That(players.Last().PlayerStats.EfficiencyRank, Is.Not.Null);
            Assert.That(players.Last().PlayerStats.EfficiencyValue, Is.Not.Null);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayerHistory_ShouldReturnHistory()
        {
            var player = await rankingApi.GetPlayerHistory(EdGiardinaPlayerId);

            Assert.That(player.PlayerId == EdGiardinaPlayerId);
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

            Assert.That(playerResults.Results, Is.Not.Null);
            Assert.That(playerResults.ResultsCount, Is.Positive);
            Assert.That(playerResults.RankingType, Is.EqualTo(rankingType));
            Assert.That(playerResults.ResultsType, Is.EqualTo(resultType));
        }

        [Test]
        public async Task PinballRankingApiV2_GetSeries_ShouldReturnSeries()
        {
            var series = await rankingApi.GetSeries();

            Assert.That(series.Count, Is.Positive);
            Assert.That(series.FirstOrDefault().Years.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetSeriesOverallStandings_ShouldReturnSeriesStandings()
        {
            var series = await rankingApi.GetSeriesOverallStanding("NACS");

            Assert.That(series.Year, Is.Positive);
            Assert.That(series.ChampionshipPrizeFund, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetSeriesRegionStandings_ShouldReturnRegionStandings()
        {
            var series = await rankingApi.GetSeriesStandingsForRegion("NACS", "MA");

            Assert.That(series.Year, Is.Positive);
            Assert.That(series.SeriesCode, Is.EqualTo("NACS"));
        }

        [Test]
        public async Task PinballRankingApiV2_GetSeriesRegionTournaments_ShouldReturnRegionTournaments()
        {
            var series = await rankingApi.GetSeriesTournamentsForRegion("NACS", "MA");

            Assert.That(series.Year, Is.Positive);
            Assert.That(series.SeriesCode, Is.EqualTo("NACS"));
        }

        [Test]
        public async Task PinballRankingApiV2_GetSeriesPlayerCard_ShouldReturnPlayerCard()
        {
            var series = await rankingApi.GetSeriesPlayerCard(16927, "NACS", "CT");

            Assert.That(series.Year, Is.Positive);
            Assert.That(series.SeriesCode, Is.EqualTo("NACS"));
        }

        [Test]
        public async Task PinballRankingApiV2_GetSeriesWinner_ShouldReturnwinner()
        {
            var series = await rankingApi.GetSeriesWinners("NACS");

            Assert.That(series.Results.Count, Is.Positive);
            Assert.That(series.SeriesCode, Is.EqualTo("NACS"));
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
            Assert.That(ranking.Rankings, Is.Not.Null);
            //TODO: this is bugged and returns +1 endpoint
            //Assert.That(ranking.Rankings.First().CountryRank, Is.EqualTo(1));
            Assert.That(ranking.Rankings.First().WpprPoints, Is.Positive);
            Assert.That(ranking.Rankings.First().CurrentWpprRank, Is.Positive);
            Assert.That(ranking.Rankings.First().EfficiencyPercent, Is.Positive);
            Assert.That(ranking.Rankings.First().EventCount, Is.Positive);
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
            var ranking = await rankingApi.GetPlayerVersusElitePlayer(elitePlayerId);

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
            Assert.That(ranking.Rankings, Is.Not.Null);
            Assert.That(ranking.Rankings.First().WpprPoints, Is.Positive);
            //TODO: Tournament Type : Women will not return current WpprRank yet.
            //Assert.That(ranking.Rankings.First().CurrentWpprRank, Is.Positive); 
        }

        [Test]
        [TestCase(TournamentType.Open)]
        [TestCase(TournamentType.Women)]
        public async Task PinballRankingApiV2_GetRankingWomenStartingAt100_ShouldReturnRanking(TournamentType tournamentType)
        {
            var ranking = await rankingApi.GetRankingForWomen(tournamentType, 100);

            Assert.That(ranking.ReturnCount, Is.Positive);
            Assert.That(ranking.TournamentType, Is.EqualTo(tournamentType));
            Assert.That(ranking.Rankings, Is.Not.Null);
            Assert.That(ranking.Rankings.First().WpprPoints, Is.Positive);
            Assert.That(ranking.Rankings.First().CurrentRank, Is.EqualTo(100));
        }

        [Test]
        [TestCase(1)]
        [TestCase(100)]
        public async Task PinballRankingApiV2_GetRankingYouth_ShouldReturnRanking(int startRank)
        {
            var ranking = await rankingApi.GetRankingForYouth(startRank, 100);

            Assert.That(ranking.ReturnCount, Is.Positive);
            Assert.That(ranking.Rankings, Is.Not.Null);
            Assert.That(ranking.Rankings.First().CurrentRank, Is.EqualTo(startRank));
            Assert.That(ranking.Rankings.First().WpprPoints, Is.Positive);
            Assert.That(ranking.Rankings.First().CurrentWpprRank, Is.Positive);            
            Assert.That(ranking.Rankings.First().EventCount, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetWpprRanking_ShouldReturnRanking()
        {
            var ranking = await rankingApi.GetWpprRanking();

            Assert.That(ranking.ReturnCount, Is.Positive);
            Assert.That(ranking.Rankings, Is.Not.Null);
            Assert.That(ranking.Rankings.First().WpprPoints, Is.Positive);
            Assert.That(ranking.Rankings.First().EfficiencyPercent, Is.Positive);
            Assert.That(ranking.Rankings.First().EventCount, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetWpprRankingAtPosition100_ShouldReturnRanking()
        {
            var ranking = await rankingApi.GetWpprRanking(100);

            Assert.That(ranking.ReturnCount, Is.Positive);
            Assert.That(ranking.Rankings, Is.Not.Null);
            Assert.That(ranking.Rankings.First().WpprPoints, Is.Positive);
            Assert.That(ranking.Rankings.First().CurrentRank, Is.EqualTo(100));
            Assert.That(ranking.Rankings.First().EfficiencyPercent, Is.Positive);
            Assert.That(ranking.Rankings.First().EventCount, Is.Positive);
        }


        [Test]
        public async Task PinballRankingApiV2_GetRankingCustom_ShouldReturnCustom()
        {
            int viewId = 68;
            var customView = await rankingApi.GetRankingCustomView(viewId);

            Assert.That(customView.TotalCount, Is.Positive);
            Assert.That(customView.ViewId, Is.EqualTo(viewId));
        }

        [Test]
        public async Task PinballRankingApiV2_GetRankingCustomWithNullRank_ShouldReturnCustom()
        {
            int viewId = 14;
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
            var tournamentId = 69374;

            var tournament = await rankingApi.GetTournament(tournamentId);

            Assert.That(tournament.TournamentId, Is.EqualTo(tournamentId));
            Assert.That(tournament.EventWeight, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetTournament_ShouldReturnTournament2()
        {
            var tournamentId = 60498;

            var tournament = await rankingApi.GetTournament(tournamentId);

            Assert.That(tournament.TournamentId, Is.EqualTo(tournamentId));
            Assert.That(tournament.EventWeight, Is.Positive);
        }



        [Test]
        public async Task PinballRankingApiV2_GetRelatedTournaments_ShouldReturnRelatedTournament()
        {
            var tournamentId = 25586;

            var tournament = await rankingApi.GetRelatedTournaments(tournamentId);

            Assert.That(tournament.First().TournamentName, Is.EqualTo("IFPA World Pinball Championship"));
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
        public async Task PinballRankingApiV2_GetTournamentResults_ShouldReturnTournamentWinners()
        {
            var tournamentId = 21076;

            var tournament = await rankingApi.GetTournamentWinners(tournamentId);

            Assert.That(tournament.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetTournamentResults_ShouldReturnTournamentWinnersGrouped()
        {
            var tournamentId = 21076;

            var tournament = await rankingApi.GetTournamentWinnersGrouped(tournamentId);

            Assert.That(tournament.Count, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetTournamentBySearch_ShouldReturnTournament()
        {
            var searchCriteria = new TournamentSearchFilter { Name = "Pinburgh" };

            var tournament = await rankingApi.GetTournamentBySearch(searchCriteria);

            Assert.That(tournament.Results.Count, Is.Positive);
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

        [Test]
        public async Task PinballRankingApiV2_GetPlayersByCountryStatistics()
        {
            var stat = await rankingApi.GetPlayersByCountryStatistics();

            var firstStat = stat.First();

            Assert.That(stat.Count, Is.Positive);
            Assert.That(firstStat.StatsRank, Is.Positive);
        }


        [Test]
        public async Task PinballRankingApiV2_GetPlayersPointsByGivenPeriod()
        {
            var stat = await rankingApi.GetPlayersPointsByGivenPeriod(DateTime.Now.AddYears(-1), DateTime.Now);

            var firstStat = stat.First();

            Assert.That(stat.Count, Is.Positive);
            Assert.That(firstStat.StatsRank, Is.Positive);
        }

        [Test]
        public async Task PinballRankingApiV2_GetPlayersEventsAttendedByGivenPeriod()
        {
            var stat = await rankingApi.GetPlayersEventsAttendedByGivenPeriod(DateTime.Now.AddYears(-1), DateTime.Now);

            var firstStat = stat.First();

            Assert.That(stat.Count, Is.Positive);
            Assert.That(firstStat.StatsRank, Is.Positive);
            Assert.That(firstStat.TournamentCount, Is.Positive);
        }

    }
}
