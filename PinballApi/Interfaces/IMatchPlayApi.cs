using PinballApi.Models.MatchPlay;
using PinballApi.Models.MatchPlay.Opdb;
using PinballApi.Models.MatchPlay.SeriesStats;
using PinballApi.Models.MatchPlay.Tournaments;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PinballApi.Interfaces
{
    /// <summary>
    /// The read-only MatchPlay Events API. It also serves the OPDB machine database and PinTips.
    /// </summary>
    /// <remarks>
    /// Register <see cref="MatchPlayApi"/> against this interface to inject the client and to
    /// replace it in tests. Every call raises <see cref="PinballApiException"/> when the service
    /// refuses it. Each <c>Enumerate</c> method walks every page of its <c>Get</c> counterpart.
    /// </remarks>
    public interface IMatchPlayApi
    {
        #region players, arenas & profiles

        Task<List<Arena>> GetArenas(Status status = Status.Active, List<int> arenaIds = null, int page = 1,
                                    CancellationToken cancellationToken = default);

        IAsyncEnumerable<Arena> EnumerateArenas(Status status = Status.Active, List<int> arenaIds = null,
                                                CancellationToken cancellationToken = default);

        Task<List<Location>> GetLocations(Status? status = null, List<int> locationIds = null, int page = 1,
                                          CancellationToken cancellationToken = default);

        IAsyncEnumerable<Location> EnumerateLocations(Status? status = null, List<int> locationIds = null,
                                                      CancellationToken cancellationToken = default);

        Task<List<Player>> GetPlayers(Status? status = null, List<int> players = null, int page = 1,
                                      CancellationToken cancellationToken = default);

        IAsyncEnumerable<Player> EnumeratePlayers(Status? status = null, List<int> players = null,
                                                  CancellationToken cancellationToken = default);

        Task<User> GetMyProfile(CancellationToken cancellationToken = default);

        Task<UserProfile> GetProfile(int playerId, CancellationToken cancellationToken = default);

        Task<List<User>> SearchForUsers(string searchText, CancellationToken cancellationToken = default);

        Task<List<Tournament>> SearchForTournaments(string searchText, CancellationToken cancellationToken = default);

        Task<List<Player>> ResolveUnknownPlayers(List<int> playerIds, CancellationToken cancellationToken = default);

        Task<List<Arena>> ResolveUnknownArenas(List<int> arenaIds, CancellationToken cancellationToken = default);

        Task<List<User>> ResolveUnknownUsers(List<int> userIds, CancellationToken cancellationToken = default);

        Task<List<Player>> ResolveUnknownTournamentPlayers(int tournamentId, List<int> playerIds, CancellationToken cancellationToken = default);

        Task<List<Arena>> ResolveUnknownTournamentArenas(int tournamentId, List<int> arenaIds, CancellationToken cancellationToken = default);

        #endregion

        #region tournaments

        Task<List<Tournament>> GetTournaments(int? ownerUserId = null, int? playedUserId = null, TournamentStatus? status = null,
                                              int? seriesId = null, int page = 1, int? limit = null,
                                              CancellationToken cancellationToken = default);

        IAsyncEnumerable<Tournament> EnumerateTournaments(int? ownerUserId = null, int? playedUserId = null, TournamentStatus? status = null,
                                                          int? seriesId = null, int? limit = null,
                                                          CancellationToken cancellationToken = default);

        Task<Tournament> GetTournament(int tournamentId, bool includePlayers = false, bool includeArenas = false, bool includeBanks = false,
                                       bool includeScorekeepers = false, bool includeSeries = false, bool includeLocation = false,
                                       bool includeRsvpConfiguration = false, bool includeParent = false, bool includePlayoffs = false,
                                       bool includeShortcut = false, bool includeEntryConfiguration = false,
                                       bool includeLinkedTournaments = false, bool includeEvent = false,
                                       CancellationToken cancellationToken = default);

        Task<List<Standing>> GetStandings(int tournamentId, CancellationToken cancellationToken = default);

        Task<List<Round>> GetRounds(int tournamentId, CancellationToken cancellationToken = default);

        Task<List<Queue>> GetQueues(int tournamentId, CancellationToken cancellationToken = default);

        Task<IfpaEstimate> GetIfpaEstimate(int? tournamentId = null, int? seriesId = null, List<int> ifpaIds = null,
                                           List<string> names = null, CancellationToken cancellationToken = default);

        Task<List<TournamentArenaSummary>> GetTournamentArenaSummary(int tournamentId, int page = 1, CancellationToken cancellationToken = default);

        IAsyncEnumerable<TournamentArenaSummary> EnumerateTournamentArenaSummary(int tournamentId, CancellationToken cancellationToken = default);

        Task<List<TournamentPlayerArenaSummary>> GetTournamentPlayerArenaSummary(int tournamentId, int page = 1, CancellationToken cancellationToken = default);

        IAsyncEnumerable<TournamentPlayerArenaSummary> EnumerateTournamentPlayerArenaSummary(int tournamentId, CancellationToken cancellationToken = default);

        Task<List<TournamentMatchSummary>> GetTournamentMatchSummary(int tournamentId, int page = 1, CancellationToken cancellationToken = default);

        IAsyncEnumerable<TournamentMatchSummary> EnumerateTournamentMatchSummary(int tournamentId, CancellationToken cancellationToken = default);

        #endregion

        #region games

        Task<List<Game>> GetGames(List<int> tournamentIds = null, int? playerId = null, int? arenaId = null, int? round = null,
                                  int? bank = null, GameStatus? gameStatus = null, List<int> seriesIds = null,
                                  List<int> gameIds = null, int page = 1, CancellationToken cancellationToken = default);

        IAsyncEnumerable<Game> EnumerateGames(List<int> tournamentIds = null, int? playerId = null, int? arenaId = null, int? round = null,
                                              int? bank = null, GameStatus? gameStatus = null, List<int> seriesIds = null,
                                              List<int> gameIds = null, CancellationToken cancellationToken = default);

        Task<TournamentGame> GetGame(int tournamentId, int gameId, CancellationToken cancellationToken = default);

        Task<List<TournamentGame>> GetTournamentGames(int tournamentId, CancellationToken cancellationToken = default);

        Task<List<SinglePlayerGame>> GetSinglePlayerGames(int tournamentId, int page = 1, int? limit = null, List<int> gameIds = null,
                                                          SinglePlayerGameStatus? status = null, bool? bestGame = null, bool? voided = null,
                                                          int? round = null, int? playerId = null, int? arenaId = null,
                                                          CancellationToken cancellationToken = default);

        IAsyncEnumerable<SinglePlayerGame> EnumerateSinglePlayerGames(int tournamentId, int? limit = null, List<int> gameIds = null,
                                                                      SinglePlayerGameStatus? status = null, bool? bestGame = null,
                                                                      bool? voided = null, int? round = null, int? playerId = null,
                                                                      int? arenaId = null, CancellationToken cancellationToken = default);

        Task<SinglePlayerGame> GetSinglePlayerGame(int tournamentId, int singlePlayerGameId, CancellationToken cancellationToken = default);

        Task<List<SinglePlayerGame>> GetTopScoresByArena(int tournamentId, CancellationToken cancellationToken = default);

        Task<List<Card>> GetCards(int tournamentId, int page = 1, int? limit = null, SinglePlayerGameStatus? status = null,
                                  bool? bestGame = null, bool? voided = null, int? playerId = null,
                                  CancellationToken cancellationToken = default);

        IAsyncEnumerable<Card> EnumerateCards(int tournamentId, int? limit = null, SinglePlayerGameStatus? status = null,
                                              bool? bestGame = null, bool? voided = null, int? playerId = null,
                                              CancellationToken cancellationToken = default);

        Task<Card> GetCard(int tournamentId, int cardId, CancellationToken cancellationToken = default);

        #endregion

        #region formats & statistics

        Task<FlipFrenzy> GetFlipFrenzy(int tournamentId, CancellationToken cancellationToken = default);

        Task<MaxMatchplay> GetMaxMatchplay(int tournamentId, CancellationToken cancellationToken = default);

        Task<MatchplayStats> GetMatchplayStats(int tournamentId, CancellationToken cancellationToken = default);

        Task<List<RoundStats>> GetRoundStats(int tournamentId, CancellationToken cancellationToken = default);

        Task<List<ArenaStats>> GetArenaStats(int tournamentId, CancellationToken cancellationToken = default);

        Task<PlayerStats> GetPlayerStats(int tournamentId, CancellationToken cancellationToken = default);

        Task<List<ArenaStats>> GetMatchStats(int tournamentId, CancellationToken cancellationToken = default);

        Task<BestGameStats> GetBestGameStats(int tournamentId, CancellationToken cancellationToken = default);

        Task<List<BestGameSummary>> GetBestGameSummary(int tournamentId, CancellationToken cancellationToken = default);

        Task<BestGame> GetBestGameDetails(int tournamentId, int arenaId, CancellationToken cancellationToken = default);

        #endregion

        #region series

        Task<List<Series>> GetSeriesList(int? ownerUserId = null, int? playedUserId = null, SeriesStatus? seriesStatus = null,
                                         int page = 1, CancellationToken cancellationToken = default);

        IAsyncEnumerable<Series> EnumerateSeries(int? ownerUserId = null, int? playedUserId = null, SeriesStatus? seriesStatus = null,
                                                 CancellationToken cancellationToken = default);

        Task<Series> GetSeries(int seriesId, CancellationToken cancellationToken = default);

        Task<List<Player>> GetSeriesAttendance(int seriesId, int count, CancellationToken cancellationToken = default);

        Task<SeriesStats> GetSeriesStats(int seriesId, CancellationToken cancellationToken = default);

        #endregion

        #region ratings

        Task<RatingComparison> ComparePlayers(List<int> playerIds = null, List<int> ifpaIds = null, List<int> userIds = null,
                                              CancellationToken cancellationToken = default);

        Task<RatingProfile> GetRatingProfile(int id, RatingQueryType ratingQueryType, CancellationToken cancellationToken = default);

        Task<List<Rating>> GetCurrentRatingData(List<int> ifpaIds = null, List<int> userIds = null, int page = 1,
                                                CancellationToken cancellationToken = default);

        Task<List<RatingPeriod>> GetRatingPeriods(int page = 1, CancellationToken cancellationToken = default);

        IAsyncEnumerable<RatingPeriod> EnumerateRatingPeriods(CancellationToken cancellationToken = default);

        Task<SingleRatingPeriod> GetRatingPeriod(DateTime date, CancellationToken cancellationToken = default);

        Task<List<IfpaRatingHistory>> GetRatingHistoryByIfpaId(int ifpaId, int limit = 100, int page = 1,
                                                               CancellationToken cancellationToken = default);

        #endregion

        #region opdb & pintips

        Task<OpdbEntry> GetOpdbEntry(string opdbId, bool includePeople = false, bool includeImages = false,
                                     CancellationToken cancellationToken = default);

        Task<List<OpdbChangelogEntry>> GetOpdbChangelog(CancellationToken cancellationToken = default);

        Task<PinTipsResult> GetPinTipsByOpdbId(string opdbId, CancellationToken cancellationToken = default);

        Task<PinTipsResult> GetPinTipsByArenaId(int arenaId, CancellationToken cancellationToken = default);

        Task<List<OpdbEntry>> GetOpdbExport(CancellationToken cancellationToken = default);

        Task<List<OpdbSlimEntry>> GetOpdbSlimExport(CancellationToken cancellationToken = default);

        Task<List<PinTip>> GetPinTipsExport(CancellationToken cancellationToken = default);

        #endregion
    }
}
