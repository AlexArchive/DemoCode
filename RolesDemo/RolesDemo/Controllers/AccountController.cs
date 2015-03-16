using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RolesDemo.Models;
using RolesDemo.Orm;

namespace RolesDemo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterInputModel model)
        {
            if (base.ModelState.IsValid)
            {
                var context = new ApplicationContext();
                var store = new UserStore<IdentityUser>(context);
                var manager = new UserManager<IdentityUser>(store);
                var user = new IdentityUser { UserName = model.UserName };
                var operation = manager.Create(user, model.Password);

                if (operation.Succeeded)
                {
                    return RedirectToAction("Login");
                }
            }
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginInputModel model)
        {
            if (base.ModelState.IsValid)
            {
                var context = new ApplicationContext();
                var store = new UserStore<IdentityUser>(context);
                var manager = new UserManager<IdentityUser>(store);
                var user = manager.Find(model.UserName, model.Password);
                if (user != null)
                {
                    var identity = manager.CreateIdentity(
                        user, 
                        DefaultAuthenticationTypes.ApplicationCookie);

                    var authenticator = HttpContext.GetOwinContext().Authentication;

                    authenticator.SignIn(identity);
                    return RedirectToAction("Index", "Home");

                }
                ModelState.AddModelError("", "Invalid username or password");
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            var authenticator = HttpContext.GetOwinContext().Authentication;
            authenticator.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}