using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using WorkWithKOTE.Filters;
using WorkWithKOTE.Models;

namespace WorkWithKOTE.Controllers
{
    public class HomeController : Controller
    {
        //TourContext db = new TourContext();
        UsersContext db = new UsersContext();
        public ActionResult Index()
        {
            var data = new TourForHomePage();
            data.TourHightPrev = db.Tour.Where(m => m.TypeOfTour == "Туры по Украине" && m.TourStatus == "Превью блока").FirstOrDefault();
            data.TourCenterPrev = db.Tour.Where(m => m.TypeOfTour == "Туры в Грузию").FirstOrDefault(m => m.TourStatus == "Превью блока");
            data.TourDownPrev = db.Tour.Where(m => m.TypeOfTour == "Приключенческие туры").FirstOrDefault(m => m.TourStatus == "Превью блока");
            data.TourHight = db.Tour.Where(m => m.TourStatus == "Активный" && m.TypeOfTour == "Туры по Украине").ToList();
            data.TourCenter = db.Tour.Where(m => m.TourStatus == "Активный" && m.TypeOfTour == "Туры в Грузию").ToList();
            data.TourDown = db.Tour.Where(m => m.TourStatus == "Активный" && m.TypeOfTour == "Приключенческие туры").ToList();
            return View(data);
        }
        public ActionResult DateForCurrentTour(int id)
        {
            
            var date = db.DateTours.Where(m => m.TourId== id);
          
            return PartialView(date);
        }
        public ActionResult NewsBlock()
        {
            return PartialView(db.News.ToList());
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
