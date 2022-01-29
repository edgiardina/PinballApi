using Flurl;
using Flurl.Http;
using PinballApi.Interfaces;
using PinballApi.Models.OPDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PinballApi
{
    public class OPDBApi : IOpdbApi
    {
        protected readonly string ApiToken;
        protected Url BaseRequest => $"https://opdb.org/api/".SetQueryParams(new { api_token = ApiToken });

        public OPDBApi(string apiToken)
        {
            this.ApiToken = apiToken;   
        }

        public async Task<PinballMachine> GetMachineInfo(string opdbId)
        {
            return await BaseRequest
                            .AppendPathSegment("machines")
                            .AppendPathSegment(opdbId)
                            .GetJsonAsync<PinballMachine>();
        }

        public async Task<PinballMachine> GetMachineInfoByIpdbId(string ipdbId)
        {
            return await BaseRequest
                            .AppendPathSegment("machines")
                            .AppendPathSegment("ipdb")
                            .AppendPathSegment(ipdbId)
                            .GetJsonAsync<PinballMachine>();
        }

        public async Task<IEnumerable<PinballMachine>> Search(string query, bool requireOpdb = true, bool includeGroups = false, bool includeAliases = true, bool includeGroupingEntries = false)
        {
            return await BaseRequest
                            .AppendPathSegment("search")
                            .SetQueryParam("q", query)
                            .SetQueryParam("require_opdb", requireOpdb)
                            .SetQueryParam("include_groups", includeGroups)
                            .SetQueryParam("include_aliases", includeAliases)
                            .SetQueryParam("include_grouping_entries", includeGroupingEntries)
                            .GetJsonAsync<IEnumerable<PinballMachine>>();
        }

        public async Task<IEnumerable<TypeAheadSearchResult>> TypeAheadSearch(string query, bool includeGroups = false, bool includeAliases = true)
        {
            return await BaseRequest
                            .AppendPathSegment("search")
                            .AppendPathSegment("typeahead")
                            .SetQueryParam("q", query)
                            .SetQueryParam("include_groups", includeGroups)
                            .SetQueryParam("include_aliases", includeAliases)
                            .GetJsonAsync<IEnumerable<TypeAheadSearchResult>>();
        }
    }
}
