using MessageBoard.Models;
using PagedList;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MessageBoard.Controllers
{
    public class HomeController : Controller
    {
        DatabaseModel db = new DatabaseModel();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewPost(Messages input)
        {
            if (input.Title != null && input.Content != null)
            {
                Messages data = new Messages()
                {
                    Title = input.Title,
                    Content = input.Content,
                    Time = DateTime.Now
                };
                db.Messages.Add(data);
                db.SaveChanges();
            }

            return RedirectToAction("RenderPosts");
        }

        public PartialViewResult RenderPosts(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            var getData = db.Messages.OrderByDescending(x => x.Time);
            var messages = getData.ToPagedList(currentPage, 6);

            return PartialView("_MessageList", messages);
        }
    }
}
