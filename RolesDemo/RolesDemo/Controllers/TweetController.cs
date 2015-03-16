using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RolesDemo.Models;
using RolesDemo.Orm;

namespace RolesDemo.Controllers
{
    [Authorize]
    public class TweetController : Controller
    {
        [ChildActionOnly]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ChildActionOnly]
        public ActionResult Create(TweetInputModel model)
        {
            var context = new ApplicationContext();
            var user = new IdentityUser { Id = User.Identity.GetUserId() };
            context.Users.Attach(user);
            var tweet = new Tweet
            {
                Text = model.Text,
                Author = user,
                PublishDateTime = DateTime.Now
            };
            context.Tweets.Add(tweet);
            context.SaveChanges();
            context.Dispose();
            return View();
        }

        public ActionResult Delete(int id)
        {
            var context = new ApplicationContext();
            var tweet = context.Tweets
                .Include(t => t.Author)
                .SingleOrDefault(t => t.Id == id);
            if (tweet == null)
            {
                return HttpNotFound();
            }
            if (User.IsInRole("Admin") 
                || tweet.Author.Id == User.Identity.GetUserId())
            {
                context.Tweets.Remove(tweet);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}