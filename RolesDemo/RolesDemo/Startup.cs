using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using RolesDemo;
using RolesDemo.Orm;

[assembly: OwinStartup(typeof(Startup))]

namespace RolesDemo
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });

            var username = "Alex";
            var password = "password";
            var roleName = "Admin";

            var context = new ApplicationContext();
            var userStore = new UserStore<IdentityUser>(context);
            var userManager = new UserManager<IdentityUser>(userStore);
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var role = new IdentityRole(roleName);

            var roleExists = roleManager.RoleExists(roleName);
            if (!roleExists)
            {
                roleManager.Create(role);
            }

            var user = userManager.FindByName(username);
            if (user == null)
            {
                user = new IdentityUser{UserName=username};
                userManager.Create(user, password);
                userManager.AddToRole(user.Id, roleName);
            }

            context.Dispose();
        }
    }
}
