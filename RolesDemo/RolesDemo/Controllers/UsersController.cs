using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RolesDemo.Models;
using RolesDemo.Orm;

namespace RolesDemo.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        public ActionResult Index()
        {
            var context = new ApplicationContext();
            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);
            var users = context.Users
                .ToList()
                .Select(user => new UserModel
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                    IsPremium = userManager.IsInRole(user.Id, "Premium")
                });
            return View(users);
        }

        public ActionResult Upgrade(string id)
        {
            var context = new ApplicationContext();
            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);
            var roleName = "Premium";
            var role = new IdentityRole(roleName);
            var roleExists = roleManager.RoleExists("Premium");
            if (!roleExists)
            {
                roleManager.Create(role);
            }
            if (userManager.IsInRole(id, roleName))
            {
                userManager.RemoveFromRole(id, roleName);
            }
            else
            {
                userManager.AddToRole(id, roleName);
            }
            context.Dispose();
            return RedirectToAction("Index");
        }
    }
}