using PinballApi.Models.WPPR.Universal;
using PinballApi.Models.WPPR.Universal.Players;
using PinballApi.Models.WPPR.Universal.Rankings;
using PinballApi.Models.WPPR.Universal.Tournaments.Search;
using PinballApi.Models.WPPR.v2.Calendar;
using System;
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
    }
}
