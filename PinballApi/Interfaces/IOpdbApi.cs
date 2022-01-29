using PinballApi.Models.OPDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PinballApi.Interfaces
{
    public interface IOpdbApi
    {
        Task<IEnumerable<TypeAheadSearchResult>> TypeAheadSearch(string query, bool includeGroups = false, bool includeAliases = true);

        Task<IEnumerable<PinballMachine>> Search(string query, bool requireOpdb = true, bool includeGroups = false, bool includeAliases = true, bool includeGroupingEntries = false);

        Task<PinballMachine> GetMachineInfo(string opdbId);

        Task<PinballMachine> GetMachineInfoByIpdbId(string ipdbId);
    }
}
