using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebApplication.Models;
using WebApplication.Persistence;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly IQuestionRepository repository;

        #region Bastard Injection
        // Do not do this in production code - create a composition root.
        public HomeController()
            : this(new QuestionRepository())
        {
        }
        #endregion

        public HomeController(IQuestionRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            IEnumerable<Question> model = repository.All().Reverse();
            return View(model);
        }

        public ActionResult AddQuestion()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion(QuestionInput question)
        {
            if (!ModelState.IsValid)
            {
                return View(question);
            }
            repository.Add(question.ToQuestion());
            return RedirectToAction("Index");
        }

        public JsonResult SearchTags(string searchTerm)
        {
            IEnumerable<string> model = repository.All()
                .SelectMany(question => question.Tags)
                .Where(tag => tag.ToLower().Contains(searchTerm.ToLower()))
                .Distinct();

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}