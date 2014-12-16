using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Pagination.Persistence;

namespace Pagination
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            Database.SetInitializer(new Seeder());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Pagination",
                url: "{controller}/{action}/{pageNumber}",
                defaults: new { controller = "Home", action = "Index", pageNumber = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
