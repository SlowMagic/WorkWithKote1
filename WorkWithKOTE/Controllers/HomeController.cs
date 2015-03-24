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
            data.TourHightPrev = db.Tour.Where(m => m.TypeOfTour == "Гастрономический" && m.TourStatus == "Превью блока").FirstOrDefault();
            data.TourCenterPrev = db.Tour.Where(m => m.TypeOfTour == "Развлекательный").FirstOrDefault(m => m.TourStatus == "Превью блока");
            data.TourDownPrev = db.Tour.Where(m => m.TypeOfTour == "Шопинг").FirstOrDefault(m => m.TourStatus == "Превью блока");
            data.TourHight = db.Tour.Where(m => m.TourStatus == "Активный" && m.TypeOfTour == "Гастрономический").ToList();
            data.TourCenter = db.Tour.Where(m => m.TourStatus == "Активный" && m.TypeOfTour == "Развлекательный").ToList();
            data.TourDown = db.Tour.Where(m => m.TourStatus == "Активный" && m.TypeOfTour == "Шопинг").ToList();
            return View(data);
        }
        public ActionResult DateForCurrentTour(int id)
        {
            
            var date = db.DateTours.Where(m => m.TourId== id);
          
            return PartialView(date);
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

        public ActionResult NewsBlock()
        {
            return PartialView();
        }
    }
}
