﻿using System;
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
        public ActionResult CreateNews()
        {
            var data = new News();
            data.NewsDate = DateTime.Now.ToUniversalTime();
            return View(data);
        }
        [HttpPost]
        public ActionResult CreateNews(News model)
        {
            model.NewsBody = Regex.Replace(model.NewsBody, "<script.*?</script>", "", RegexOptions.IgnoreCase);
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult NewsBlock()
        {
            return View(db.News.ToList());
        }
    }
}