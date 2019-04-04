using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F1GrandPrix.Models.Users.Infrastructure;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using F1GrandPrix.Models.Users.Models;
using System.Threading.Tasks;
using F1GrandPrix.Models.DataBase;
using F1GrandPrix.Models.Paging;
using F1GrandPrix.Filter;
using System.Data.Entity;

namespace F1GrandPrix.Models.Users.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View(UserManager.Users);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result =
                    await UserManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);

            if (user != null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Пользователь не найден" });
            }
        }

        public async Task<ActionResult> Edit(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Edit(string id, string email, string password)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if (user != null)
            {
                user.Email = email;
                IdentityResult validEmail
                    = await UserManager.UserValidator.ValidateAsync(user);

                if (!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }

                IdentityResult validPass = null;
                if (password != string.Empty)
                {
                    validPass
                        = await UserManager.PasswordValidator.ValidateAsync(password);

                    if (validPass.Succeeded)
                    {
                        user.PasswordHash =
                            UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }

                if ((validEmail.Succeeded && validPass == null) ||
                        (validEmail.Succeeded && password != string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Пользователь не найден");
            }
            return View(user);
        }

        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
        [Authorize(Roles = "Administrators")]
        public ActionResult AdminCatalog(String teamName, String driverSurname, TeamFilter teamFilter = null, bool restore = false, int page = 1)
        {
            //ViewBag.Message = "Catalog";
            F1Context db = new F1Context();
            IQueryable<Team> teams = db.Teams;

            if (teamFilter == null)
                teamFilter = new TeamFilter();

            if (restore)
                teamFilter = TeamFilter.GetTeamFilter(Session["TeamFilter"]);

            if (teamName != "" && teamName != null)
                teams = teams.Where(e => e.Name.Equals(teamName));
            if (driverSurname != "" && driverSurname != null)
                teams = teams.Where(e => e.Driver.Surname.Equals(driverSurname) || e.Driver1.Surname.Equals(driverSurname));

            //page_team = teams.ToList();
            int pageSize = 4;

            //IEnumerable<Team> teamsPerPages = page_team.Take(pageSize);
            IEnumerable<Team> teamsPerPages = teams.ToList().Skip((page - 1) * pageSize).Take(pageSize);
            PageInfo pgInfo = new PageInfo { PageNumber = page, PageSize = pageSize, TotalItems = teams.ToList().Count };
            IndexView ivm = new IndexView { pageInfo = pgInfo, Teams = teamsPerPages };

            Session["TeamFilter"] = teamFilter;
            ivm.teamFilter = teamFilter;

            return View(ivm);
        }
        
        [Authorize(Roles = "Administrators")]
        public ActionResult AdminIndex()
        {
            return View("~/Views/Admin/AdminIndex.cshtml");
        }
    }
}