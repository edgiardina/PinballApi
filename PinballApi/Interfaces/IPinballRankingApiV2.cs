using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.v2.Directors;
using PinballApi.Models.WPPR.v2.Players;
using PinballApi.Models.WPPR.v2.Rankings;
using PinballApi.Models.WPPR.v2.Series;
using PinballApi.Models.WPPR.v2.Stats;
using PinballApi.Models.WPPR.v2.Tournaments;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PinballApi.Interfaces
{
    public interface IPinballRankingApiV2
    {
        Task<List<Director>> GetCountryDirectors(CancellationToken cancellationToken = default);
        Task<List<Director>> GetDirector(int directorId, CancellationToken cancellationToken = default);
        Task<List<Director>> GetDirectorList(CancellationToken cancellationToken = default);
        Task<EliteRanking> GetEliteRanking(int startPosition = 1, int count = 50, CancellationToken cancellationToken = default);
        Task<List<EventsByYearStatistics>> GetEventsByYearStatistics(CancellationToken cancellationToken = default);
        Task<List<LargestTournamentStatistics>> GetLargestTournamentStatistics(CancellationToken cancellationToken = default);
        Task<List<LucrativeTournamentStatistics>> GetLucrativeTournamentStatistics(CancellationToken cancellationToken = default);
        Task<List<Director>> GetNacsDirectors(CancellationToken cancellationToken = default);
        Task<OverallStatistics> GetOverallStatistics(CancellationToken cancellationToken = default);
        Task<Player> GetPlayer(int playerId, CancellationToken cancellationToken = default);
        Task<PlayerHistory> GetPlayerHistory(int playerId, CancellationToken cancellationToken = default);
        Task<PlayerResults> GetPlayerResults(int playerId, RankingType rankingType = RankingType.Main, ResultType resultType = ResultType.Active, CancellationToken cancellationToken = default);
        Task<List<Player>> GetPlayers(List<int> playerIds, CancellationToken cancellationToken = default);
        Task<List<PlayersByCountryStatistics>> GetPlayersByCountryStatistics(CancellationToken cancellationToken = default);
        Task<PlayerSearch> GetPlayersBySearch(PlayerSearchFilter searchFilter, CancellationToken cancellationToken = default);
        Task<List<PlayersByStateStatistics>> GetPlayersByStateStatistics(CancellationToken cancellationToken = default);
        Task<List<PlayersByYearStatistics>> GetPlayersByYearStatistics(CancellationToken cancellationToken = default);
        Task<List<PlayersEventsAttendedByGivenPeriodStatistics>> GetPlayersEventsAttendedByGivenPeriod(DateTime startDate, DateTime endDate, int limit = 25, CancellationToken cancellationToken = default);
        Task<List<PlayersPointsByGivenPeriodStatistics>> GetPlayersPointsByGivenPeriod(DateTime startDate, DateTime endDate, int limit = 25, CancellationToken cancellationToken = default);
        Task<ElitePlayerVersusPlayer> GetPlayerVersusElitePlayer(int elitePlayerId, CancellationToken cancellationToken = default);
        Task<PlayerVersusPlayer> GetPlayerVersusPlayer(int playerId, CancellationToken cancellationToken = default);
        Task<PlayerVersusPlayerComparison> GetPlayerVersusPlayerComparison(int playerId, int comparisonPlayerId, CancellationToken cancellationToken = default);
        Task<CountryList> GetRankingCountries(CancellationToken cancellationToken = default);
        Task<CustomRanking> GetRankingCustomView(int viewId, CancellationToken cancellationToken = default);
        Task<CustomRankingList> GetRankingCustomViewList(CancellationToken cancellationToken = default);
        Task<CountryRanking> GetRankingForCountry(string countryName, int startPosition = 1, int count = 50, CancellationToken cancellationToken = default);
        Task<WomensRanking> GetRankingForWomen(TournamentType tournamentType, int startPosition = 1, int count = 50, CancellationToken cancellationToken = default);
        Task<YouthRanking> GetRankingForYouth(int startPosition = 1, int count = 50, CancellationToken cancellationToken = default);
        Task<List<Models.WPPR.v2.Tournaments.Tournament>> GetRelatedTournaments(int tournamentId, CancellationToken cancellationToken = default);
        Task<List<Series>> GetSeries(CancellationToken cancellationToken = default);
        Task<SeriesOverallResults> GetSeriesOverallStanding(string seriesCode, int? year = null, CancellationToken cancellationToken = default);
        Task<SeriesPlayerCard> GetSeriesPlayerCard(int playerId, string seriesCode, string region, int? year = null, CancellationToken cancellationToken = default);
        Task<RegionStandings> GetSeriesStandingsForRegion(string seriesCode, string region, int? year = null, CancellationToken cancellationToken = default);
        Task<SeriesTournaments> GetSeriesTournamentsForRegion(string seriesCode, string region, int? year = null, CancellationToken cancellationToken = default);
        Task<SeriesWinners> GetSeriesWinners(string seriesCode, string region = null, CancellationToken cancellationToken = default);
        Task<Models.WPPR.v2.Tournaments.Tournament> GetTournament(int tournamentId, CancellationToken cancellationToken = default);
        Task<TournamentSearch> GetTournamentBySearch(TournamentSearchFilter searchFilter, CancellationToken cancellationToken = default);
        Task<TournamentResults> GetTournamentResults(int tournamentId, CancellationToken cancellationToken = default);
        Task<List<TournamentsByStateStatistics>> GetTournamentsByStateStatistics(CancellationToken cancellationToken = default);
        Task<List<Models.WPPR.v2.Tournaments.TournamentWinner>> GetTournamentWinners(int tournamentId, CancellationToken cancellationToken = default);
        Task<List<TournamentWinnerGrouped>> GetTournamentWinnersGrouped(int tournamentId, CancellationToken cancellationToken = default);
        Task<WpprRanking> GetWpprRanking(int startPosition = 1, int count = 50, CancellationToken cancellationToken = default);
    }
}