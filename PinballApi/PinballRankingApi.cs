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
using PinballApi.Models.WPPR.Universal.Tournaments;

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

        public async Task<TournamentSearch> TournamentSearch(double? latitude = null, double? longitude = null, int? radius = null, DistanceType? distanceType = null, string name = null, string country = null, string stateprov = null, DateTime? startDate = null, DateTime? endDate = null, RankingSystem? rankingSystem = null, int? startPosition = null,
            int? totalReturn = null, TournamentSearchSortMode? tournamentSearchSortMode = null, TournamentSearchSortOrder? tournamentSearchSortOrder = null, string directorName = null,
            bool? preRegistration = null, bool? onlyWithResults = null, double? minimumPoints = null, double? maximumPoints = null, bool? pointFilter = null)
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

            if (rankingSystem.HasValue)
                request = request.SetQueryParam("rank_type", rankingSystem.Value.ToString().ToUpper());

            if(radius.HasValue)
                request = request.SetQueryParam("radius", radius);

            if (startPosition.HasValue)
                request = request.SetQueryParam("start_pos", startPosition);

            if (totalReturn.HasValue)
                request = request.SetQueryParam("total", totalReturn);

            if (tournamentSearchSortMode.HasValue)
                request = request.SetQueryParam("sort", tournamentSearchSortMode.Value.ToString().ToUpper());

            if (tournamentSearchSortOrder.HasValue)
            {
                if (tournamentSearchSortOrder == TournamentSearchSortOrder.Ascending)
                    request = request.SetQueryParam("order", "ASC");
                else
                    request = request.SetQueryParam("order", "DESC");
            }

            if (minimumPoints.HasValue)
                request = request.SetQueryParam("min_points", minimumPoints);

            if (maximumPoints.HasValue)
                request = request.SetQueryParam("max_points", maximumPoints);

            if (pointFilter.HasValue)
            {
                if(pointFilter == true)
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
                    request = request.SetQueryParam("only_results", "Y");
                else
                    request = request.SetQueryParam("only_results", "N");
            }

            if (!string.IsNullOrEmpty(directorName))
                request = request.SetQueryParam("director_name", directorName);

            return await request.GetJsonAsync<TournamentSearch>();
        }

        public async Task<Models.WPPR.Universal.Tournaments.Tournament> GetTournament(int tournamentId)
        {
            var request = BaseRequest
                .AppendPathSegment($"tournament/{tournamentId}");

            return await request.GetJsonAsync<Models.WPPR.Universal.Tournaments.Tournament>();
        }
    }
}
