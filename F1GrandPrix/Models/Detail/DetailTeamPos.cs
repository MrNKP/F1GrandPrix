using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using F1GrandPrix.Models.DataBase;

namespace F1GrandPrix.Models.Detail
{
    public class DetailTeamPos
    {
        public int teamID { get; set; }
        public Team team { get; set; }
    }
}