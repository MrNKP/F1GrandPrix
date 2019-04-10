using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using F1GrandPrix.Models.DataBase;
using F1GrandPrix.Models.Paging;
using F1GrandPrix.Models.Detail;
using F1GrandPrix.Filter;

namespace F1GrandPrix.Controllers
{
    public class HomeController : Controller
    {
        private F1Context db = new F1Context();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "История программы";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Разработчик";

            return View();
        }

        public List<Team> page_team;
        public ActionResult Catalog(String teamName, String driverSurname, TeamFilter teamFilter = null, bool restore = false, int page = 1)
        {
            //ViewBag.Message = "Catalog";
            //F1Context db = new F1Context();
            IQueryable<Team> teams = db.Teams;

            if (teamFilter == null)
                teamFilter = new TeamFilter();

            if (restore)
                teamFilter = TeamFilter.GetTeamFilter(Session["TeamFilter"]);

            if (teamName != "" && teamName != null)
                teams = teams.Where(e => e.Name.Equals(teamName));
            if (driverSurname != "" && driverSurname != null)
                teams = teams.Where(e => e.Driver.Surname.Equals(driverSurname) || e.Driver1.Surname.Equals(driverSurname));

            page_team = teams.ToList();
            int pageSize = 4;

            //IEnumerable<Team> teamsPerPages = page_team.Take(pageSize);
            IEnumerable<Team> teamsPerPages = page_team.Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pgInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = page_team.Count };
            IndexView ivm = new IndexView { pageInfo = pgInfo, Teams = teamsPerPages};

            Session["TeamFilter"] = teamFilter;
            ivm.teamFilter = teamFilter;

            return View(ivm);
        }
        public ActionResult Detail()
        {
            ViewBag.Message = "Подробнее о команде";
            DetailTeam myTeam = DetailTeam.GetCart(Session["MyTeam"]);
            List<DetailTeamPos> lines = (List<DetailTeamPos>)myTeam.Lines;
            return View(lines);
        }
        public ActionResult AddToDetail(int? ID=1)
        {
            F1Context db = new F1Context();
            Team t = db.Teams.Find(ID);
            DetailTeam myTeam = DetailTeam.GetCart(Session["MyTeam"]);
            myTeam.AddItem(t.ID, t);
            Session["MyTeam"] = myTeam;
            //return Redirect("/Home/Catalog/?restoreFilter=true");
            return Redirect("/Home/Detail");
        }
        public ActionResult ClearDetail()
        {
            DetailTeam myTeam = DetailTeam.GetCart(Session["MyTeam"]);
            myTeam.ClearDetail();
            return Redirect("/Home/Detail");
        }
    }
}