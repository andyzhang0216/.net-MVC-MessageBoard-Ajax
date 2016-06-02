using MessageBoard.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MessageBoard.Controllers
{
    public class HomeController : Controller
    {
        DatabaseModel db = new DatabaseModel();

        public ActionResult Index(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;
            var getData = db.Messages.OrderByDescending(x => x.Time);
            var messages = getData.ToPagedList(currentPage, 6);

            return View(messages);
        }

        [HttpPost]
        public PartialViewResult AjaxPost(Messages input, int page = 1)
        {
            Messages data = new Messages()
            {
                Title = input.Title,
                Content = input.Content,
                Time = DateTime.Now
            };
            db.Messages.Add(data);
            db.SaveChanges();

            int currentPage = page < 1 ? 1 : page;
            var getData = db.Messages.OrderByDescending(x => x.Time);
            var messages = getData.ToPagedList(currentPage, 6);

            return PartialView("_MessageList", messages);
        }

        public PartialViewResult AjaxPost(int page = 1)
        {
            int currentPage = page < 1 ? 1 : page;

            var getData = db.Messages.OrderByDescending(x => x.Time);
            var messages = getData.ToPagedList(currentPage, 6);

            return PartialView("_MessageList", messages);
        }
    }
}