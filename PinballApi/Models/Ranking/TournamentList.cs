using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballApi.Models.Ranking
{
    public class TournamentList
    {
        [JsonProperty("tournament")]
        public IList<Tournament> Tournament { get; set; }

        [JsonProperty("Total_Results")]
        public string TotalResults { get; set; }
    }
}
