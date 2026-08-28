using PinballApi.Models.WPPR.v1.Calendar;
using PinballApi.Models.WPPR.v1.Players;
using PinballApi.Models.WPPR.v1.Pvp;
using PinballApi.Models.WPPR.v1.Rankings;
using PinballApi.Models.WPPR.v1.Statistics;
using PinballApi.Models.WPPR.v1.Tournaments;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PinballApi.Models.WPPR;
using PinballApi.Interfaces;
using System.Runtime.Serialization;
using System.Text.Json.Nodes;
using System.Text.Json;

namespace PinballApi
{
    public class PinballRankingApiV1 : BasePinballRankingApi, IPinballRankingApiV1
    {
        protected override PinballRankingApiVersion ApiVersion => PinballRankingApiVersion.v1;

        public PinballRankingApiV1(string apiKey) : base(apiKey)
        {

        }

        #region player

        public async Task<PlayerRecord> GetPlayerRecord(int playerId, CancellationToken cancellationToken = default)
        {
            try
            {
                return
                 await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment(playerId)
                    .GetJsonAsync<PlayerRecord>(cancellationToken: cancellationToken);
            }
            catch (FlurlHttpException ex) when (ex.InnerException is JsonException)
            {
                //Indicates null values which may mean invalid playerId
                return null;
            }
        }

        public async Task<PlayerResult> GetPlayerResults(int playerId, CancellationToken cancellationToken = default)
        {
            return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment(playerId)
                .AppendPathSegment("results")
                .GetJsonAsync<PlayerResult>(cancellationToken: cancellationToken);
        }

        public async Task<PlayerComparisons> GetPlayerComparisons(int playerId, CancellationToken cancellationToken = default)
        {
            return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment(playerId)
                .AppendPathSegment("pvp")
                .GetJsonAsync<PlayerComparisons>(cancellationToken: cancellationToken);
        }

        public async Task<PlayerSearch> SearchForPlayerByName(string name, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrEmpty(name))
                throw new ArgumentNullException("Name cannot be null");

            try
            {
                return await BaseRequest
                    .AppendPathSegment("player")
                    .AppendPathSegment("search")
                    .SetQueryParam("q", name)
                    .GetJsonAsync<PlayerSearch>(cancellationToken: cancellationToken);
            }
            catch (FlurlParsingException)
            {
                //admittedly this is a bit hacky. Might be better to have a custom converted on PlayerSearch's Search list but that gets tricky.
                return new PlayerSearch
                {
                    Query = name,
                    Search = new List<Search>()
                };
            }
        }

        public async Task<PlayerSearch> SearchForPlayerByEmail(string email, CancellationToken cancellationToken = default)
        {
            if (String.IsNullOrEmpty(email))
                throw new ArgumentNullException("Email cannot be null");
            try
            {
                return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment("search")
                .SetQueryParam("email", email)
                .GetJsonAsync<PlayerSearch>(cancellationToken: cancellationToken);
            }
            catch (FlurlParsingException ex)
            {
                //admittedly this is a bit hacky. Might be better to have a custom converted on PlayerSearch's Search list but that gets tricky.
                if (ex.InnerException.Message.Contains("No players found"))
                {
                    return new PlayerSearch
                    {
                        Query = email,
                        Search = new List<Search>()
                    };
                }

                throw;
            }
        }

        public async Task<PlayerSearch> GetCountryDirectors(CancellationToken cancellationToken = default)
        {
            return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment("country_directors")
                .GetJsonAsync<PlayerSearch>(cancellationToken: cancellationToken);
        }

        public async Task<PlayerHistory> GetPlayerHistory(int playerId, CancellationToken cancellationToken = default)
        {
            return await BaseRequest
                .AppendPathSegment("player")
                .AppendPathSegment(playerId)
                .AppendPathSegment("history")
                .GetJsonAsync<PlayerHistory>(cancellationToken: cancellationToken);
        }

        #endregion

        #region tournament

        public async Task<Tournament> GetTournament(int tournamentId, CancellationToken cancellationToken = default)
        {
            var json = await BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment(tournamentId)
                .GetStringAsync(cancellationToken: cancellationToken);

            return JsonNode.Parse(json)["tournament"].Deserialize<Tournament>(JsonSerializerOptions);
        }

        public async Task<TournamentResult> GetTournamentResults(int tournamentId, int? eventId = null, DateTime? tournamentDate = null, CancellationToken cancellationToken = default)
        {
            var request = BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment(tournamentId)
                .AppendPathSegment("results");

            if (eventId.HasValue)
                request = request.SetQueryParam("event_id", eventId.Value);

            if (tournamentDate.HasValue)
                request = request.SetQueryParam("tour_date", tournamentDate.Value.ToString("yyyy-MM-dd"));

            var json = await request.GetStringAsync(cancellationToken: cancellationToken);

            return JsonNode.Parse(json)["tournament"].Deserialize<TournamentResult>(JsonSerializerOptions);
        }

        public async Task<TournamentList> GetTournamentList(int startPosition = 1, int count = 50, CancellationToken cancellationToken = default)
        {
            if (count > 250 || count < 1)
                throw new ArgumentException("Count must be less than or equal to 250", "count");

            if (startPosition < 1)
                throw new ArgumentException("Start Positon must be a positive integer", "startPosition");

            var request = BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment("list");

            if (startPosition > 1)
                request.SetQueryParam("start_pos", startPosition);

            if (count != 50)
                request.SetQueryParam("count", count);

            return await request.GetJsonAsync<TournamentList>(cancellationToken: cancellationToken);
        }

        public async Task<TournamentSearch> TournamentSearch(string tournamentName, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(tournamentName))
                throw new ArgumentNullException("Tournament Name cannot be null or empty");

            return await BaseRequest
                .AppendPathSegment("tournament")
                .AppendPathSegment("search")
                .SetQueryParam("q", tournamentName)
                .GetJsonAsync<TournamentSearch>(cancellationToken: cancellationToken);
        }

        #endregion

        #region ranking

        public async Task<RankingList> GetRankings(int startPosition = 1, int count = 50, RankingOrder order = RankingOrder.points, string countryName = null, CancellationToken cancellationToken = default)
        {
            return await BaseRequest
                .AppendPathSegment("rankings")
                .SetQueryParams(new
                {
                    start_pos = startPosition,
                    count = count,
                    order = order,
                    country = countryName
                })
                .GetJsonAsync<RankingList>(cancellationToken: cancellationToken);
        }

        #endregion

        #region calendar

        public async Task<CalendarList> GetActiveCalendar(string country = null, CancellationToken cancellationToken = default)
        {
            var request = BaseRequest
                .AppendPathSegment("calendar")
                .AppendPathSegment("active");

            if (!string.IsNullOrEmpty(country))
                request = request.SetQueryParam("country", country);

            return await request.GetJsonAsync<CalendarList>(cancellationToken: cancellationToken);
        }

        public async Task<CalendarList> GetCalendarHistory(string country = null, CancellationToken cancellationToken = default)
        {
            var request = BaseRequest
                .AppendPathSegment("calendar")
                .AppendPathSegment("history");

            if (!string.IsNullOrEmpty(country))
                request = request.SetQueryParam("country", country);

            return await request.GetJsonAsync<CalendarList>(cancellationToken: cancellationToken);
        }

        public async Task<CalenderItem> GetCalendarById(int calendarId, CancellationToken cancellationToken = default)
        {
            return await BaseRequest
               .AppendPathSegment("calendar")
               .AppendPathSegment(calendarId)
               .GetJsonAsync<CalenderItem>(cancellationToken: cancellationToken);
        }

        public async Task<CalendarSearch> GetCalendarSearch(string address, int distance, DistanceUnit units, CancellationToken cancellationToken = default)
        {
            var request = BaseRequest
               .AppendPathSegment("calendar")
               .AppendPathSegment("search")
               .SetQueryParam("address", address);

            if (units == DistanceUnit.Kilometers)
                request = request.SetQueryParam("k", distance);
            else
                request = request.SetQueryParam("m", distance);

            return await request.GetJsonAsync<CalendarSearch>(cancellationToken: cancellationToken);
        }

        #endregion

        #region pvp

        public async Task<PlayerComparison> GetPvp(int playerOneId, int playerTwoId, CancellationToken cancellationToken = default)
        {
            return await BaseRequest
               .AppendPathSegment("pvp")
               .SetQueryParams(new
               {
                   p1 = playerOneId,
                   p2 = playerTwoId
               })
               .GetJsonAsync<PlayerComparison>(cancellationToken: cancellationToken);
        }

        #endregion

        #region stats

        public async Task<List<PointsThisYearStat>> GetPointsThisYearStats(CancellationToken cancellationToken = default)
        {
            var json = await BaseRequest
               .AppendPathSegment("stats")
               .AppendPathSegment("points_this_year")
               .GetStringAsync(cancellationToken: cancellationToken);

            return JsonNode.Parse(json)["stats"].Deserialize<List<PointsThisYearStat>>(JsonSerializerOptions);
        }

        public async Task<List<MostEventsStat>> GetMostEventsStats(CancellationToken cancellationToken = default)
        {
            var json = await BaseRequest
                .AppendPathSegment("stats")
                .AppendPathSegment("most_events")
                .GetStringAsync(cancellationToken: cancellationToken);

            return JsonNode.Parse(json)["stats"].Deserialize<List<MostEventsStat>>(JsonSerializerOptions);
        }

        public async Task<List<PlayersByCountryStat>> GetPlayersByCountryStat(CancellationToken cancellationToken = default)
        {
            var json = await BaseRequest
                .AppendPathSegment("stats")
                .AppendPathSegment("country_players")
                .GetStringAsync(cancellationToken: cancellationToken);

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersByCountryStat>>(JsonSerializerOptions);
        }

        public async Task<List<EventsByYearStat>> GetEventsPerYearStat(CancellationToken cancellationToken = default)
        {
            var json = await BaseRequest
                .AppendPathSegment("stats")
                .AppendPathSegment("events_by_year")
                .GetStringAsync(cancellationToken: cancellationToken);

            return JsonNode.Parse(json)["stats"].Deserialize<List<EventsByYearStat>>(JsonSerializerOptions);
        }

        public async Task<List<PlayersByYearStat>> GetPlayersPerYearStat(CancellationToken cancellationToken = default)
        {
            var json = await BaseRequest
                .AppendPathSegment("stats")
                .AppendPathSegment("players_by_year")
                .GetStringAsync(cancellationToken: cancellationToken);

            return JsonNode.Parse(json)["stats"].Deserialize<List<PlayersByYearStat>>(JsonSerializerOptions);
        }

        public async Task<List<BiggestMoversStat>> GetBiggestMoversStat(CancellationToken cancellationToken = default)
        {
            var json = await BaseRequest
                .AppendPathSegment("stats")
                .AppendPathSegment("biggest_movers")
                .GetStringAsync(cancellationToken: cancellationToken);

            return JsonNode.Parse(json)["stats"].Deserialize<List<BiggestMoversStat>>(JsonSerializerOptions);
        }

        #endregion

    }
}
