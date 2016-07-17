
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PinballApi.Models.Ranking
{
    public class PlayerSearch
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("search")]
        public IList<Search> Search { get; set; }
    }
}
