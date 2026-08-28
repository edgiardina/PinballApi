using PinballApi.Models.WPPR.v1.Calendar;
using PinballApi.Models.WPPR.v1.Players;
using PinballApi.Models.WPPR.v1.Pvp;
using PinballApi.Models.WPPR.v1.Rankings;
using PinballApi.Models.WPPR.v1.Statistics;
using PinballApi.Models.WPPR.v1.Tournaments;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PinballApi.Interfaces
{
    public interface IPinballRankingApiV1
    {
        Task<CalendarList> GetActiveCalendar(string country = null, CancellationToken cancellationToken = default);
        Task<List<BiggestMoversStat>> GetBiggestMoversStat(CancellationToken cancellationToken = default);
        Task<CalenderItem> GetCalendarById(int calendarId, CancellationToken cancellationToken = default);
        Task<CalendarList> GetCalendarHistory(string country = null, CancellationToken cancellationToken = default);
        Task<CalendarSearch> GetCalendarSearch(string address, int distance, DistanceUnit units, CancellationToken cancellationToken = default);
        Task<PlayerSearch> GetCountryDirectors(CancellationToken cancellationToken = default);
        Task<List<EventsByYearStat>> GetEventsPerYearStat(CancellationToken cancellationToken = default);
        Task<List<MostEventsStat>> GetMostEventsStats(CancellationToken cancellationToken = default);
        Task<PlayerComparisons> GetPlayerComparisons(int playerId, CancellationToken cancellationToken = default);
        Task<PlayerHistory> GetPlayerHistory(int playerId, CancellationToken cancellationToken = default);
        Task<PlayerRecord> GetPlayerRecord(int playerId, CancellationToken cancellationToken = default);
        Task<PlayerResult> GetPlayerResults(int playerId, CancellationToken cancellationToken = default);
        Task<List<PlayersByCountryStat>> GetPlayersByCountryStat(CancellationToken cancellationToken = default);
        Task<List<PlayersByYearStat>> GetPlayersPerYearStat(CancellationToken cancellationToken = default);
        Task<List<PointsThisYearStat>> GetPointsThisYearStats(CancellationToken cancellationToken = default);
        Task<PlayerComparison> GetPvp(int playerOneId, int playerTwoId, CancellationToken cancellationToken = default);
        Task<RankingList> GetRankings(int startPosition = 1, int count = 50, RankingOrder order = RankingOrder.points, string countryName = null, CancellationToken cancellationToken = default);
        Task<Tournament> GetTournament(int tournamentId, CancellationToken cancellationToken = default);
        Task<TournamentList> GetTournamentList(int startPosition = 1, int count = 50, CancellationToken cancellationToken = default);
        Task<TournamentResult> GetTournamentResults(int tournamentId, int? eventId = null, DateTime? tournamentDate = null, CancellationToken cancellationToken = default);
        Task<PlayerSearch> SearchForPlayerByEmail(string email, CancellationToken cancellationToken = default);
        Task<PlayerSearch> SearchForPlayerByName(string name, CancellationToken cancellationToken = default);
        Task<TournamentSearch> TournamentSearch(string tournamentName, CancellationToken cancellationToken = default);
    }
}