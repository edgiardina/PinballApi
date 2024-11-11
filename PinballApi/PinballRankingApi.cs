using Flurl.Http;
using Flurl;
using PinballApi.Models.WPPR;
using System;
using System.Threading.Tasks;
using Flurl.Http.Configuration;
using PinballApi.Interfaces;
using PinballApi.Models.WPPR.v2.Calendar;
using PinballApi.Models.WPPR.Universal;
using PinballApi.Models.WPPR.Universal.Tournaments.Search;
using PinballApi.Models.WPPR.Universal.Rankings;
using PinballApi.Models.WPPR.Universal.Players;
using System.Text.Json.Nodes;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;
using PinballApi.Models.WPPR.Universal.Players.Search;
using PinballApi.Models.WPPR.Universal.Series;

namespace PinballApi
{
    public class PinballRankingApi : BasePinballRankingApi, IPinballRankingApi
    {
        public PinballRankingApi(string apiKey) : base(apiKey)
        {

        }

        protected override IFlurlRequest BaseRequest => $"https://api.ifpapinball.com/"
                              .SetQueryParams(new { api_key = ApiKey })
                              .WithSettings(settings =>
                              {
                                  settings.JsonSerializer = new DefaultJsonSerializer(JsonSerializerOptions);
                              });

        protected override PinballRankingApiVersion ApiVersion => PinballRankingApiVersion.Universal;

        #region Tournaments

        public async Task<TournamentSearch> TournamentSearch(double? latitude = null, double? longitude = null, int? radius = null, DistanceType? distanceType = null, string name = null, string country = null, string stateprov = null, DateTime? startDate = null, DateTime? endDate = null, TournamentType? tournamentType = null, int? startPosition = null,
            int? totalReturn = null, TournamentSearchSortMode? tournamentSearchSortMode = null, TournamentSearchSortOrder? tournamentSearchSortOrder = null, string directorName = null,
            bool? preRegistration = null, bool? onlyWithResults = null, double? minimumPoints = null, double? maximumPoints = null, bool? pointFilter = null, TournamentEventType? tournamentEventType = null)
        {

            var request = BaseRequest
                .AppendPathSegment("tournament/search");

            if (distanceType == DistanceType.Kilometers)
                request = request.SetQueryParam("k", radius);
            else if (distanceType == DistanceType.Miles)
                request = request.SetQueryParam("m", radius);

            if (latitude != 0 && longitude != 0)
            {
                request = request.SetQueryParam("latitude", latitude);
                request = request.SetQueryParam("longitude", longitude);
            }

            if (!string.IsNullOrEmpty(name))
                request = request.SetQueryParam("name", name);

            if (!string.IsNullOrEmpty(country))
                request = request.SetQueryParam("country", country);

            if (!string.IsNullOrEmpty(stateprov))
                request = request.SetQueryParam("stateprov", stateprov);

            if (startDate.HasValue)
                request = request.SetQueryParam("start_date", startDate.Value.ToString("yyyy-MM-dd"));

            if (endDate.HasValue)
                request = request.SetQueryParam("end_date", endDate.Value.ToString("yyyy-MM-dd"));

            if (tournamentType.HasValue)
            {
                //Tournament type must be MAIN or WOMEN
                if (tournamentType != TournamentType.Main && tournamentType != TournamentType.Women)
                    throw new ArgumentException("Tournament Type must be MAIN or WOMEN");

                request = request.SetQueryParam("rank_type", tournamentType.Value.ToString().ToUpper());
            }

            if (radius.HasValue)
                request = request.SetQueryParam("radius", radius);

            if (startPosition.HasValue)
                request = request.SetQueryParam("start_pos", startPosition);

            if (totalReturn.HasValue)
                request = request.SetQueryParam("total", totalReturn);

            if (tournamentSearchSortMode.HasValue)
                request = request.SetQueryParam("sort_mode", tournamentSearchSortMode.Value.ToString().ToUpper());

            if (tournamentSearchSortOrder.HasValue)
            {
                if (tournamentSearchSortOrder == TournamentSearchSortOrder.Ascending)
                    request = request.SetQueryParam("sort_order", "ASC");
                else
                    request = request.SetQueryParam("sort_order", "DESC");
            }

            if (minimumPoints.HasValue)
                request = request.SetQueryParam("min_points", minimumPoints);

            if (maximumPoints.HasValue)
                request = request.SetQueryParam("max_points", maximumPoints);

            if (pointFilter.HasValue)
            {
                if (pointFilter == true)
                    request = request.SetQueryParam("point_filter", "Y");
                else
                    request = request.SetQueryParam("point_filter", "N");
            }

            if (preRegistration.HasValue)
            {
                if (preRegistration == true)
                    request = request.SetQueryParam("preregistration", "Y");
                else
                    request = request.SetQueryParam("preregistration", "N");
            }

            if (onlyWithResults.HasValue)
            {
                if (onlyWithResults == true)
                    request = request.SetQueryParam("only_with_results", "Y");
                else
                    request = request.SetQueryParam("only_with_results", "N");
            }

            if (!string.IsNullOrEmpty(directorName))
                request = request.SetQueryParam("director_name", directorName);

            if (tournamentEventType.HasValue)
                request = request.SetQueryParam("event_type", tournamentEventType.Value.ToString().ToUpper());

            return await request.GetJsonAsync<TournamentSearch>();
        }

        public async Task<Models.WPPR.Universal.Tournaments.Tournament> GetTournament(int tournamentId)
        {
            var request = BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment(tournamentId);

            return await request.GetJsonAsync<Models.WPPR.Universal.Tournaments.Tournament>();
        }

        #endregion

        #region Rankings

        public async Task<RankingCountries> GetRankingCountries()
        {
            var request = BaseRequest
                .AppendPathSegment("rankings/country_list");

            return await request.GetJsonAsync<RankingCountries>();
        }

        public async Task<RankingSearch> RankingSearch(RankingType rankingType, RankingSystem rankingSystem = RankingSystem.Open, int count = 100, int startPosition = 1, string countryCode = null)
        {
            if (rankingType == RankingType.Pro)
                throw new ArgumentException("Use Pro Ranking Search method for Pro Rankings");

            if (rankingType == RankingType.Country && string.IsNullOrEmpty(countryCode))
                throw new ArgumentException("Country Code must be provided for Country Rankings");

            var request = BaseRequest
                .AppendPathSegment("rankings")
                .AppendPathSegment(rankingType.ToString().ToLower())
                .SetQueryParams(new
                {
                    start_pos = startPosition,
                    count
                });

            if (rankingType == RankingType.Women)
            {
                request = request.AppendPathSegment(rankingSystem.ToString().ToLower());
            }
            else if (rankingType == RankingType.Country)
            {
                request = request.SetQueryParam("country", countryCode);
            }

            return await request.GetJsonAsync<RankingSearch>();
        }

        public async Task<ProRankingSearch> ProRankingSearch(TournamentType rankingSystem = TournamentType.Open)
        {
            if (rankingSystem == TournamentType.Youth)
                throw new ArgumentException("Youth Pro Rankings are not supported");

            if (rankingSystem == TournamentType.Main)
                rankingSystem = TournamentType.Open;

            var request = BaseRequest
                            .AppendPathSegment("rankings")
                            .AppendPathSegment("pro")
                            .AppendPathSegment(rankingSystem.ToString().ToLower());

            return await request.GetJsonAsync<ProRankingSearch>();
        }

        #endregion

        #region Players

        public async Task<Player> GetPlayer(int playerId)
        {
            var json = await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment(playerId)
                .GetStringAsync();

            return JsonNode.Parse(json)["player"].Deserialize<List<Player>>(JsonSerializerOptions).First();
        }

        public async Task<PlayerSearch> PlayerSearch(string name = null, string country = null)
        {
            if (string.IsNullOrEmpty(name) && string.IsNullOrEmpty(country))
                throw new ArgumentException("Name or Country must be provided");

            var request = BaseRequest
                .AppendPathSegment("player/search");

            if (!string.IsNullOrEmpty(name))
                request = request
                .SetQueryParam("name", name);

            if (!string.IsNullOrEmpty(country))
                request = request
                .SetQueryParam("country", country);

            return await request.GetJsonAsync<PlayerSearch>();
        }

        public async Task<PlayerHistory> GetPlayerHistory(int playerId, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main, bool activeResultsOnly = false)
        {
            var request = BaseRequest
                            .AppendPathSegment("player")
                            .AppendPathSegment(playerId)
                            .AppendPathSegment("rank_history");

            request = request.SetQueryParam("system", playerSystem.ToString().ToUpper());

            if (activeResultsOnly)
                request = request.SetQueryParam("active_flag", "Y");

            return await request.GetJsonAsync<PlayerHistory>();
        }

        public async Task<PlayerVersusPlayer> GetPlayerVersusPlayer(int playerId, PlayerRankingSystem playerSystem = PlayerRankingSystem.Main)
        {
            var request = BaseRequest
                            .AppendPathSegment("player")
                            .AppendPathSegment(playerId)
                            .AppendPathSegment("pvp");

            request = request.SetQueryParam("system", playerSystem.ToString().ToUpper());

            return await request.GetJsonAsync<PlayerVersusPlayer>();
        }

        /// <summary>
        /// Get a player's top 20 results for a region within a series
        /// </summary>
        /// <param name="playerId">Player's id</param>
        /// <param name="seriesCode">Series abbreviation (e.g. NACS)</param>
        /// <param name="region">Abbreviation for region (e.g. MA for Massachusetts)</param>
        /// <param name="year">Optional year (current year is default)</param>
        /// <returns></returns>
        public async Task<SeriesPlayerCard> GetSeriesPlayerCard(int playerId, string seriesCode, string region, int? year = null)
        {
            var request = BaseRequest
                .AppendPathSegment("series")
                .AppendPathSegment(seriesCode.ToUpper())
                .AppendPathSegment("player_card")
                .AppendPathSegment(playerId)
                .SetQueryParam("region_code", region);

            if (year.HasValue)
            {
                request.SetQueryParam("year", year.Value);
            }

            return await request.GetJsonAsync<SeriesPlayerCard>();
        }

        #endregion
    }
}
