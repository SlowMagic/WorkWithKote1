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
            data.TourHight = db.Tour.Where(m => m.TypeOfTour == "Гастрономический").ToList();
            data.TourCenter = db.Tour.Where(m => m.TypeOfTour == "Развлекательный").ToList();
            data.TourDown = db.Tour.Where(m => m.TypeOfTour =="Шопинг").ToList();
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
        public ActionResult MainTour(int id)
        {
            Tour data = new Tour();
            if (id == 1)
                data = db.Tour.Where(m => m.TypeOfTour == "Гастрономический").First(m => m.TourStatus == "Превью блока");
            if (id == 2)
                data = db.Tour.Where(m => m.TypeOfTour == "Развлекательный").First(m => m.TourStatus == "Превью блока");
            if (id == 3)
                data = db.Tour.Where(m => m.TypeOfTour == "Шопинг").First(m => m.TourStatus == "Превью блока");
            return PartialView(data);
        }
        public ActionResult SubTour(int id)
        {
            var data = db.Tour.AsQueryable();
            if (id == 1)
                data = db.Tour.Where(m => m.TourStatus == "Активный").Where(m => m.TypeOfTour == "Гастрономический");
            if (id == 2)
                data = db.Tour.Where(m => m.TourStatus == "Активный").Where(m => m.TypeOfTour == "Развлекательный");
            if (id == 3)
                data = db.Tour.Where(m => m.TourStatus == "Активный").Where(m => m.TypeOfTour == "Шопинг");
            return PartialView(data);
        }
    }
}
