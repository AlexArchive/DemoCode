using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Pagination.Persistence;

namespace Pagination
{
    public class HomeController : Controller
    {
        public ActionResult Index(int pageNumber = 1)
        {
            var context = new ApplicationContext();
            var posts = context.Posts.ToList();

            var model = new PostPage(posts, pageNumber, pageSize: 5);
            return View(model);
        }
    }

    public class PostPage : List<Post>
    {
        public int PageNumber { get; set; }
        public int TotalPageCount { get; set; }

        public PostPage(IEnumerable<Post> source, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            TotalPageCount = (int) Math.Ceiling(source.Count() / (double) pageSize);

            var parition = source
                .Skip((PageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            AddRange(parition);
        }

        public bool HasPreviousPage
        {
            get { return PageNumber > 1; }
        }

        public bool HasNextPage
        {
            get { return PageNumber < TotalPageCount; }

        }
    }
}