using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using System.Collections.Generic;
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
    }
}
