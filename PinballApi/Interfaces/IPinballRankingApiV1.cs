using PinballApi.Models.WPPR.v1.Calendar;
using PinballApi.Models.WPPR.v1.Players;
using PinballApi.Models.WPPR.v1.Pvp;
using PinballApi.Models.WPPR.v1.Rankings;
using PinballApi.Models.WPPR.v1.Statistics;
using PinballApi.Models.WPPR.v1.Tournaments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PinballApi.Interfaces
{
    public interface IPinballRankingApiV1
    {
        Task<CalendarList> GetActiveCalendar(string country = null);
        Task<List<BiggestMoversStat>> GetBiggestMoversStat();
        Task<CalenderItem> GetCalendarById(int calendarId);
        Task<CalendarList> GetCalendarHistory(string country = null);
        Task<CalendarSearch> GetCalendarSearch(string address, int distance, DistanceUnit units);
        Task<PlayerSearch> GetCountryDirectors();
        Task<List<EventsByYearStat>> GetEventsPerYearStat();
        Task<List<MostEventsStat>> GetMostEventsStats();
        Task<PlayerComparisons> GetPlayerComparisons(int playerId);
        Task<PlayerHistory> GetPlayerHistory(int playerId);
        Task<PlayerRecord> GetPlayerRecord(int playerId);
        Task<PlayerResult> GetPlayerResults(int playerId);
        Task<List<PlayersByCountryStat>> GetPlayersByCountryStat();
        Task<List<PlayersByYearStat>> GetPlayersPerYearStat();
        Task<List<PointsThisYearStat>> GetPointsThisYearStats();
        Task<PlayerComparison> GetPvp(int playerOneId, int playerTwoId);
        Task<RankingList> GetRankings(int startPosition = 1, int count = 50, RankingOrder order = RankingOrder.points, string countryName = null);
        Task<Tournament> GetTournament(int tournamentId);
        Task<TournamentList> GetTournamentList(int startPosition = 1, int count = 50);
        Task<TournamentResult> GetTournamentResults(int tournamentId, int? eventId = null, DateTime? tournamentDate = null);
        Task<PlayerSearch> SearchForPlayerByEmail(string email);
        Task<PlayerSearch> SearchForPlayerByName(string name);
        Task<TournamentSearch> TournamentSearch(string tournamentName);
    }
}