using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using F1GrandPrix.Models.DataBase;
using F1GrandPrix.Filter;

namespace F1GrandPrix.Models.Paging
{
    public class IndexView
    {
        public IEnumerable<Team> Teams { get; set; }
        public PageInfo pageInfo { get; set; }
        public TeamFilter teamFilter { get; set; }
    }
}