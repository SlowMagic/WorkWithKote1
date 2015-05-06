using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
using System.Data;
using System.Threading;
namespace WorkWithKOTE.Controllers
{
    public class NewsController : Controller
    {
        //
        // GET: /News/
        UsersContext db = new UsersContext();
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult CreateNews()
        {
            var data = new News();
            data.NewsDate = DateTime.Now.ToUniversalTime();
            return View(data);
        }
        [HttpPost]
        public ActionResult CreateNews(News model)
        {
            if (model.NewsBody == null)
                model.NewsBody = "Новость пустая";
            model.NewsBody = Regex.Replace(model.NewsBody, "<script.*?</script>", "", RegexOptions.IgnoreCase);
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult NewsBlock(int id)
        {
            return View(db.News.Find(id));
        }
        public ActionResult AllNewsBlock( )
        {
            List<News> news = db.News.ToList();
            news.Reverse();
            return View(news);
        }
        public ActionResult NewsDelete(int id = 0)
        {
           if(id != 0)
           {
               var data = db.News.Find(id);
               db.Entry(data).State = EntityState.Deleted;
               db.SaveChanges();
           }
           return RedirectToAction("AllNewsBlock","News");
        }
    }
}
