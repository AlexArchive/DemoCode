using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Pagination.Persistence;

namespace Pagination
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new ApplicationContext();
            var model = context.Posts.ToList();
            return View(model);
        }
    }
}