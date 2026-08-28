using PinballApi.Models.WPPR;
using PinballApi.Models.WPPR.Universal;
using PinballApi.Models.WPPR.Universal.Director;
using PinballApi.Models.WPPR.Universal.Directors;
using PinballApi.Models.WPPR.Universal.Players;
using PinballApi.Models.WPPR.Universal.Players.Search;
using PinballApi.Models.WPPR.Universal.Rankings;
using PinballApi.Models.WPPR.Universal.Rankings.Custom;
using PinballApi.Models.WPPR.Universal.Series;
using PinballApi.Models.WPPR.Universal.Stats;
using PinballApi.Models.WPPR.Universal.Tournaments;
using PinballApi.Models.WPPR.Universal.Tournaments.Search;
using PinballApi.Models.WPPR.Universal.Other;
using PinballApi.Models.WPPR.Universal.Tournaments.Related;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PinballApi.Interfaces
{
    public interface IPinballRankingApi
    {
        Task<RankingSearch> RankingSearch(RankingType rankingType, RankingSystem rankingSystem = RankingSystem.Open, int count = 100, int startPosition = 1, string countryCode = null, CancellationToken cancellationToken = default);
        Task<Models.WPPR.Universal.Tournaments.Tournament> GetTournament(int tournamentId, CancellationToken cancellationToken = default);
        Task<TournamentSearch> TournamentSearch(double? latitude = null, double? longitude = null, int? radius = null, DistanceType? distanceType = null, string name = null, string country = null, string stateprov = null, DateTime? startDate = null, DateTime? endDate = null, TournamentType? tournamentType = null, int? startPosition = null, int? totalReturn = null, TournamentSearchSortMode? tournamentSearchSortMode = null, TournamentSearchSortOrder? tournamentSearchSortOrder = null, string directorName = null, bool? preRegistration = null, bool? onlyWithResults = null, double? minimumPoints = null, double? maximumPoints = null, bool? pointFilter = null, TournamentEventType? tournamentEventType = null, CancellationToken cancellationToken = default);
        Task<Player> GetPlayer(int playerId, CancellationToken cancellationToken = default);
        Task<RankingCountries> GetRankingCountries(CancellationToken cancellationToken = default);
        Task<ProRankingSearch> ProRankingSearch(TournamentType rankingSystem, CancellationToken cancellationToken = default);
        Task<SeriesPlayerCard> GetSeriesPlayerCard(int playerId, string seriesCode, string region, int? year = null, CancellationToken cancellationToken = default);
        Task<PlayerHistory> GetPlayerHistory(int playerId, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, bool activeResultsOnly = false, CancellationToken cancellationToken = default);
        Task<PlayerVersusPlayer> GetPlayerVersusPlayer(int playerId, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, CancellationToken cancellationToken = default);
        Task<PlayerVersusPlayerComparison> GetPlayerVersusPlayerComparison(int playerId, int comparisonPlayerId, CancellationToken cancellationToken = default);
        Task<List<Series>> GetSeries(CancellationToken cancellationToken = default);
        Task<SeriesOverallResults> GetSeriesOverallStanding(string seriesCode, int? year = null, CancellationToken cancellationToken = default);
        Task<RegionStandings> GetSeriesStandingsForRegion(string seriesCode, string region, int? year = null, CancellationToken cancellationToken = default);
        Task<SeriesTournaments> GetSeriesTournamentsForRegion(string seriesCode, string region, int? year = null, CancellationToken cancellationToken = default);
        Task<SeriesWinners> GetSeriesWinners(string seriesCode, string region = null, CancellationToken cancellationToken = default);
        Task<PlayerResults> GetPlayerResults(int playerId, PlayerRankingSystem rankingSystem = PlayerRankingSystem.Main, ResultType resultType = ResultType.Active, CancellationToken cancellationToken = default);
        Task<List<Player>> GetPlayers(List<int> playerIds, CancellationToken cancellationToken = default);
        Task<List<CountryDirector>> GetCountryDirectors(CancellationToken cancellationToken = default);
        Task<PlayerSearch> PlayerSearch(string name = null, string country = null, string stateProv = null, string tournamentName = null, int? tournamentPosition = null, CancellationToken cancellationToken = default);
        Task<OverallStatistics> GetOverallStatistics(CancellationToken cancellationToken = default);
        Task<List<EventsByYearStatistics>> GetEventsByYearStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, CancellationToken cancellationToken = default);
        Task<List<LargestTournamentStatistics>> GetLargestTournamentStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, CancellationToken cancellationToken = default);
        Task<List<LucrativeTournamentStatistics>> GetLucrativeTournamentStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, CancellationToken cancellationToken = default);
        Task<List<PlayersByYearStatistics>> GetPlayersByYearStatistics(CancellationToken cancellationToken = default);
        Task<List<PlayersByStateStatistics>> GetPlayersByStateStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, CancellationToken cancellationToken = default);
        Task<List<TournamentsByStateStatistics>> GetTournamentsByStateStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, CancellationToken cancellationToken = default);
        Task<List<PlayersByCountryStatistics>> GetPlayersByCountryStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, CancellationToken cancellationToken = default);
        Task<List<PlayersEventsAttendedByGivenPeriodStatistics>> GetPlayersEventsAttendedByGivenPeriod(DateOnly startDate, DateOnly endDate, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, int limit = 25, CancellationToken cancellationToken = default);
        Task<List<PlayersPointsByGivenPeriodStatistics>> GetPlayersPointsByGivenPeriod(DateOnly startDate, DateOnly endDate, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, int limit = 25, CancellationToken cancellationToken = default);
        Task<Director> GetDirector(long directorId, CancellationToken cancellationToken = default);
        Task<List<Director>> GetDirectorsBySearch(string name, int count = 50, CancellationToken cancellationToken = default);
        Task<List<Models.WPPR.Universal.Tournaments.Tournament>> GetDirectorTournaments(long directorId, TimePeriod timePeriod, CancellationToken cancellationToken = default);
        Task<TournamentFormats> GetTournamentFormats(CancellationToken cancellationToken = default);
        Task<TournamentResults> GetTournamentResults(int tournamentId, CancellationToken cancellationToken = default);
        Task<List<RelatedTournament>> GetRelatedTournaments(int tournamentId, CancellationToken cancellationToken = default);
        Task<List<League>> GetLeagues(LeagueTimePeriod timePeriod, CancellationToken cancellationToken = default);
        Task<List<CustomRankingView>> GetCustomRankings(CancellationToken cancellationToken = default);
        Task<CustomRankingViewResult> GetCustomRankingViewResult(int viewId, int count = 50, int startPosition = 1, CancellationToken cancellationToken = default);
        Task<List<Region>> GetRegions(string seriesCode, int year, CancellationToken cancellationToken = default);
        Task<List<RegionRepresentative>> GetRegionReps(string seriesCode, CancellationToken cancellationToken = default);
        Task<SeriesStats> GetSeriesStats(string seriesCode, string region, int? year = null, CancellationToken cancellationToken = default);
        Task<List<CountryDetail>> GetCountriesList(CancellationToken cancellationToken = default);
        Task<List<StateProvCountry>> GetStateProvList(CancellationToken cancellationToken = default);
    }
}
