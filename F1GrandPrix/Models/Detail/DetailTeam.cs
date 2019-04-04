using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using F1GrandPrix.Models.DataBase;

namespace F1GrandPrix.Models.Detail
{
    public class DetailTeam
    {
        public DetailTeam() { }
        private List<DetailTeamPos> lineCollection = new List<DetailTeamPos>();
        public void AddItem(int teamid, Team tm)
        {
            //lineCollection.Add(new DetailTeamPos{teamid});
            //lineCollection.Clear();
            ClearDetail();
            lineCollection.Add(new DetailTeamPos { teamID = teamid, team = tm });
        }
        public void ClearDetail()
        {
            lineCollection.Clear();
        }
        public static DetailTeam GetCart(object s)
        {
            DetailTeam myCart = (DetailTeam)s;
            if (myCart == null)
            {
                myCart = new DetailTeam();
                s = myCart;
            }
            return myCart;
        }
        public IEnumerable<DetailTeamPos> Lines
        {
            get { return lineCollection; }
        }
    }
}