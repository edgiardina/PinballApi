using PinballApi.Models.MatchPlay;
using PinballApi.Models.MatchPlay.Opdb;
using PinballApi.Models.MatchPlay.SeriesStats;
using PinballApi.Models.MatchPlay.Tournaments;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PinballApi.Interfaces
{
    /// <summary>
    /// The read-only MatchPlay Events API. It also serves the OPDB machine database and PinTips.
    /// </summary>
    /// <remarks>
    /// Register <see cref="MatchPlayApi"/> against this interface to inject the client and to
    /// replace it in tests.
    /// </remarks>
    public interface IMatchPlayApi
    {
        #region players, arenas & profiles

        Task<List<Arena>> GetArenas(Status status = Status.Active, List<string> arenaIds = null, int page = 1);

        Task<List<Location>> GetLocations(Status? status = null, List<int> locationIds = null, int page = 1);

        Task<List<Player>> GetPlayers(Status? status = null, List<int> players = null, int page = 1);

        Task<User> GetMyProfile();

        Task<UserProfile> GetProfile(int playerId);

        Task<List<User>> SearchForUsers(string searchText);

        Task<List<Tournament>> SearchForTournaments(string searchText);

        Task<List<Player>> ResolveUnknownPlayers(List<int> playerIds);

        Task<List<Arena>> ResolveUnknownArenas(List<int> arenaIds);

        Task<List<User>> ResolveUnknownUsers(List<int> userIds);

        Task<List<Player>> ResolveUnknownTournamentPlayers(int tournamentId, List<int> playerIds);

        Task<List<Arena>> ResolveUnknownTournamentArenas(int tournamentId, List<int> arenaIds);

        #endregion

        #region tournaments

        Task<List<Tournament>> GetTournaments(int? ownerUserId = null, int? playedUserId = null, TournamentStatus? status = null, int? seriesId = null, int page = 1, int? limit = null);

        Task<Tournament> GetTournament(int tournamentId, bool includePlayers = false, bool includeArenas = false, bool includeBanks = false, bool includeScorekeepers = false, bool includeSeries = false,
                                       bool includeLocation = false, bool includeRsvpConfiguration = false, bool includeParent = false, bool includePlayoffs = false, bool includeShortcut = false,
                                       bool includeEntryConfiguration = false, bool includeLinkedTournaments = false, bool includeEvent = false);

        Task<List<Standing>> GetStandings(int tournamentId);

        Task<List<Round>> GetRounds(int tournamentId);

        Task<List<Queue>> GetQueues(int tournamentId);

        Task<IfpaEstimate> GetIfpaEstimate(int? tournamentId = null, int? seriesId = null, List<int> ifpaIds = null, List<string> names = null);

        Task<List<TournamentArenaSummary>> GetTournamentArenaSummary(int tournamentId, int page = 1);

        Task<List<TournamentPlayerArenaSummary>> GetTournamentPlayerArenaSummary(int tournamentId, int page = 1);

        Task<List<TournamentMatchSummary>> GetTournamentMatchSummary(int tournamentId, int page = 1);

        #endregion

        #region games

        Task<List<Game>> GetGames(List<int> tournamentIds = null, int? playerId = null, int? arenaId = null, int? round = null, int? bank = null, GameStatus? gameStatus = null,
                                  List<int> seriesIds = null, List<int> gameIds = null, int page = 1);

        Task<Game> GetGame(int tournamentId, int gameId);

        Task<List<SinglePlayerGame>> GetSinglePlayerGames(int tournamentId, int page = 1, int? limit = null, List<int> gameIds = null,
                                                          SinglePlayerGameStatus? status = null, bool? bestGame = null, bool? voided = null,
                                                          int? round = null, int? playerId = null, int? arenaId = null);

        Task<SinglePlayerGame> GetSinglePlayerGame(int tournamentId, int singlePlayerGameId);

        Task<List<SinglePlayerGame>> GetTopFiveScoresByArena(int tournamentId);

        Task<List<Card>> GetCards(int tournamentId, int page = 1, int? limit = null, SinglePlayerGameStatus? status = null,
                                  bool? bestGame = null, bool? voided = null, int? playerId = null);

        Task<Card> GetCard(int tournamentId, int cardId);

        Task<List<MatchplayGames>> GetMatchplayGames(int tournamentId);

        Task<MatchplayGames> GetMatchplayGame(int tournamentId, int gameId);

        #endregion

        #region formats & statistics

        Task<FlipFrenzy> GetFlipFrenzy(int tournamentId);

        Task<MaxMatchplay> GetMaxMatchplay(int tournamentId);

        Task<MatchplayStats> GetMatchplayStats(int tournamentId);

        Task<List<RoundStats>> GetMatchplayRoundStats(int tournamentId);

        Task<List<ArenaStats>> GetMatchplayArenaStats(int tournamentId);

        Task<PlayerStats> GetMatchplayPlayerStats(int tournamentId);

        Task<List<ArenaStats>> GetMatchplayMatchesStats(int tournamentId);

        Task<BestGameStats> GetBestGameStats(int tournamentId);

        Task<List<BestGameSummary>> GetBestGameSummary(int tournamentId);

        Task<BestGame> GetBestGameDetails(int tournamentId, int arenaId);

        #endregion

        #region series

        Task<List<Series>> GetSeries(int? ownerUserId = null, int? playedUserId = null, SeriesStatus? seriesStatus = null, int page = 1);

        Task<Series> GetSeries(int seriesId);

        Task<List<Player>> GetSeriesAttendance(int seriesId, int count);

        Task<SeriesStats> GetSeriesStats(int seriesId);

        #endregion

        #region ratings

        Task<RatingComparison> ComparePlayers(List<int> playerIds = null, List<int> ifpaIds = null, List<int> userIds = null);

        Task<RatingProfile> GetRatingProfile(int id, RatingQueryType ratingQueryType);

        Task<List<Rating>> GetCurrentRatingData(List<int> ifpaIds = null, List<int> userIds = null, int page = 1);

        Task<List<RatingPeriod>> GetRatingPeriods(int page = 1);

        Task<SingleRatingPeriod> GetRatingPeriod(DateTime date);

        Task<List<IfpaRatingHistory>> GetRatingHistoryByIfpaId(int ifpaId, int limit = 100, int page = 1);

        #endregion

        #region opdb & pintips

        Task<OpdbEntry> GetOpdbEntry(string opdbId, bool includePeople = false, bool includeImages = false);

        Task<List<OpdbChangelogEntry>> GetOpdbChangelog();

        Task<PinTipsResult> GetPinTipsByOpdbId(string opdbId);

        Task<PinTipsResult> GetPinTipsByArenaId(int arenaId);

        Task<List<OpdbEntry>> GetOpdbExport();

        Task<List<OpdbSlimEntry>> GetOpdbSlimExport();

        Task<List<PinTip>> GetPinTipsExport();

        #endregion
    }
}
