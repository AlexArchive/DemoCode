using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RolesDemo.Models;
using RolesDemo.Orm;

namespace RolesDemo.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new ApplicationContext();
            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);
            var tweets = context.Tweets
                .Include(tweet => tweet.Author)
                .OrderByDescending(tweet => tweet.PublishDateTime)
                .ToList()
                .Select(tweet => new TweetModel
                {
                    Id = tweet.Id,
                    Text = tweet.Text,
                    AuthorUserName = tweet.Author.UserName,
                    AuthorIsPremium = userManager.IsInRole(tweet.Author.Id, "Premium")
                });
            return View(tweets);
        }
    }
}