using System.Collections.Generic;

namespace PinballApi.Models.WPPR.Universal.Rankings
{
    public class ProRankingSearch
    {
        public RankingType RankingType { get; set; }

        public List<ProRanking> Rankings { get; set; }
    }
}
