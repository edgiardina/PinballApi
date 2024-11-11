using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.v2.Directors;
using PinballApi.Models.WPPR.v2.Players;
using PinballApi.Models.WPPR.v2.Rankings;
using PinballApi.Models.WPPR.v2.Series;
using PinballApi.Models.WPPR.v2.Stats;
using PinballApi.Models.WPPR.v2.Tournaments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PinballApi.Interfaces
{
    public interface IPinballRankingApiV2
    {
        Task<List<Director>> GetCountryDirectors();
        Task<List<Director>> GetDirector(int directorId);
        Task<List<Director>> GetDirectorList();
        Task<EliteRanking> GetEliteRanking(int startPosition = 1, int count = 50);
        Task<List<EventsByYearStatistics>> GetEventsByYearStatistics();
        Task<List<LargestTournamentStatistics>> GetLargestTournamentStatistics();
        Task<List<LucrativeTournamentStatistics>> GetLucrativeTournamentStatistics();
        Task<List<Director>> GetNacsDirectors();
        Task<OverallStatistics> GetOverallStatistics();
        Task<Player> GetPlayer(int playerId);
        Task<PlayerHistory> GetPlayerHistory(int playerId);
        Task<PlayerResults> GetPlayerResults(int playerId, RankingType rankingType = RankingType.Main, ResultType resultType = ResultType.Active);
        Task<List<Player>> GetPlayers(List<int> playerIds);
        Task<List<PlayersByCountryStatistics>> GetPlayersByCountryStatistics();
        Task<PlayerSearch> GetPlayersBySearch(PlayerSearchFilter searchFilter);
        Task<List<PlayersByStateStatistics>> GetPlayersByStateStatistics();
        Task<List<PlayersByYearStatistics>> GetPlayersByYearStatistics();
        Task<List<PlayersEventsAttendedByGivenPeriodStatistics>> GetPlayersEventsAttendedByGivenPeriod(DateTime startDate, DateTime endDate, int limit = 25);
        Task<List<PlayersPointsByGivenPeriodStatistics>> GetPlayersPointsByGivenPeriod(DateTime startDate, DateTime endDate, int limit = 25);
        Task<ElitePlayerVersusPlayer> GetPlayerVersusElitePlayer(int elitePlayerId);
        Task<PlayerVersusPlayer> GetPlayerVersusPlayer(int playerId);
        Task<PlayerVersusPlayerComparison> GetPlayerVersusPlayerComparison(int playerId, int comparisonPlayerId);
        Task<CountryList> GetRankingCountries();
        Task<CustomRanking> GetRankingCustomView(int viewId);
        Task<CustomRankingList> GetRankingCustomViewList();
        Task<CountryRanking> GetRankingForCountry(string countryName, int startPosition = 1, int count = 50);
        Task<WomensRanking> GetRankingForWomen(TournamentType tournamentType, int startPosition = 1, int count = 50);
        Task<YouthRanking> GetRankingForYouth(int startPosition = 1, int count = 50);
        Task<List<Models.WPPR.v2.Tournaments.Tournament>> GetRelatedTournaments(int tournamentId);
        Task<List<Series>> GetSeries();
        Task<SeriesOverallResults> GetSeriesOverallStanding(string seriesCode, int? year = null);
        Task<SeriesPlayerCard> GetSeriesPlayerCard(int playerId, string seriesCode, string region, int? year = null);
        Task<RegionStandings> GetSeriesStandingsForRegion(string seriesCode, string region, int? year = null);
        Task<SeriesTournaments> GetSeriesTournamentsForRegion(string seriesCode, string region, int? year = null);
        Task<SeriesWinners> GetSeriesWinners(string seriesCode, string region = null);
        Task<Models.WPPR.v2.Tournaments.Tournament> GetTournament(int tournamentId);
        Task<TournamentSearch> GetTournamentBySearch(TournamentSearchFilter searchFilter);
        Task<TournamentResults> GetTournamentResults(int tournamentId);
        Task<List<TournamentsByStateStatistics>> GetTournamentsByStateStatistics();
        Task<List<Models.WPPR.v2.Tournaments.TournamentWinner>> GetTournamentWinners(int tournamentId);
        Task<List<TournamentWinnerGrouped>> GetTournamentWinnersGrouped(int tournamentId);
        Task<WpprRanking> GetWpprRanking(int startPosition = 1, int count = 50);
    }
}