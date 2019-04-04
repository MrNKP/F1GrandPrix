using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F1GrandPrix.Filter
{
    public class TeamFilter
    {
        public String name { get; set; }
        public String driver1 { get; set; }
        public String driver2 { get; set; }
        public TeamFilter()
        {
            name = null;
            driver1 = null;
            driver2 = null;
        }
        public static TeamFilter GetTeamFilter (object s)
        {
            TeamFilter filter = (TeamFilter)s;
            if (filter == null) // если фильтр пуст, то создаем новый объект
            {
                filter = new TeamFilter();
                s = filter;
            }
            return filter;
        }
    }
}