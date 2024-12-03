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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PinballApi.Interfaces
{
    public interface IPinballRankingApi
    {
        Task<RankingSearch> RankingSearch(RankingType rankingType, RankingSystem rankingSystem = RankingSystem.Open, int count = 100, int startPosition = 1, string countryCode = null);
        Task<Models.WPPR.Universal.Tournaments.Tournament> GetTournament(int tournamentId);
        Task<TournamentSearch> TournamentSearch(double? latitude = null, double? longitude = null, int? radius = null, DistanceType? distanceType = null, string name = null, string country = null, string stateprov = null, DateTime? startDate = null, DateTime? endDate = null, TournamentType? tournamentType = null, int? startPosition = null, int? totalReturn = null, TournamentSearchSortMode? tournamentSearchSortMode = null, TournamentSearchSortOrder? tournamentSearchSortOrder = null, string directorName = null, bool? preRegistration = null, bool? onlyWithResults = null, double? minimumPoints = null, double? maximumPoints = null, bool? pointFilter = null, TournamentEventType? tournamentEventType = null);
        Task<Player> GetPlayer(int playerId);
        Task<RankingCountries> GetRankingCountries();
        Task<ProRankingSearch> ProRankingSearch(TournamentType rankingSystem);
        Task<SeriesPlayerCard> GetSeriesPlayerCard(int playerId, string seriesCode, string region, int? year = null);
        Task<PlayerHistory> GetPlayerHistory(int playerId, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, bool activeResultsOnly = false);
        Task<PlayerVersusPlayer> GetPlayerVersusPlayer(int playerId, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main);
        Task<PlayerVersusPlayerComparison> GetPlayerVersusPlayerComparison(int playerId, int comparisonPlayerId);
        Task<List<Series>> GetSeries();
        Task<SeriesOverallResults> GetSeriesOverallStanding(string seriesCode, int? year = null);
        Task<RegionStandings> GetSeriesStandingsForRegion(string seriesCode, string region, int? year = null);
        Task<SeriesTournaments> GetSeriesTournamentsForRegion(string seriesCode, string region, int? year = null);
        Task<SeriesWinners> GetSeriesWinners(string seriesCode, string region = null);
        Task<PlayerResults> GetPlayerResults(int playerId, PlayerRankingSystem rankingSystem = PlayerRankingSystem.Main, ResultType resultType = ResultType.Active);
        Task<List<Player>> GetPlayers(List<int> playerIds);
        Task<List<CountryDirector>> GetCountryDirectors();
        Task<PlayerSearch> PlayerSearch(string name = null, string country = null, string stateProv = null, string tournamentName = null);
        Task<OverallStatistics> GetOverallStatistics();
        Task<List<EventsByYearStatistics>> GetEventsByYearStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main);
        Task<List<LargestTournamentStatistics>> GetLargestTournamentStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main);
        Task<List<LucrativeTournamentStatistics>> GetLucrativeTournamentStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main);
        Task<List<PlayersByYearStatistics>> GetPlayersByYearStatistics();
        Task<List<PlayersByStateStatistics>> GetPlayersByStateStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main);
        Task<List<TournamentsByStateStatistics>> GetTournamentsByStateStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main);
        Task<List<PlayersByCountryStatistics>> GetPlayersByCountryStatistics(PlayerRankingSystem playerSystem = PlayerRankingSystem.Main);
        Task<List<PlayersEventsAttendedByGivenPeriodStatistics>> GetPlayersEventsAttendedByGivenPeriod(DateOnly startDate, DateOnly endDate, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, int limit = 25);
        Task<List<PlayersPointsByGivenPeriodStatistics>> GetPlayersPointsByGivenPeriod(DateOnly startDate, DateOnly endDate, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, int limit = 25);
        Task<Director> GetDirector(long directorId);
        Task<List<Director>> GetDirectorsBySearch(string name, int count = 50);
        Task<Models.WPPR.Universal.Tournaments.Tournament> GetDirectorTournaments(long directorId, TimePeriod timePeriod);
        Task<TournamentFormats> GetTournamentFormats();
        Task<TournamentResults> GetTournamentResults(int tournamentId);
        Task<List<TournamentResult>> GetRelatedResults(int tournamentId);
        Task<List<League>> GetLeagues(LeagueTimePeriod timePeriod);
        Task<List<CustomRankingView>> GetCustomRankings();
        Task<CustomRankingViewResult> GetCustomRankingViewResult(int viewId, int count = 50);
        Task<List<Region>> GetRegions(string seriesCode, int year);
        Task<List<RegionRepresentative>> GetRegionReps(string seriesCode);
    }
}
