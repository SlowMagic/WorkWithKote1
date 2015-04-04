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
using System.Data.Entity;
namespace WorkWithKOTE.Controllers
{
    public class HomeController : Controller
    {
        //TourContext db = new TourContext();
        UsersContext db = new UsersContext();
        protected Tour TourBlock(int tourStatusId,int typeofTourId)
        {
            var date = db.Tour.Include(m => m.DateTour)
                .Where(m => m.TourStatusId == tourStatusId && m.TypeOfTourId == typeofTourId)
                .SelectMany(m => m.DateTour)
                .Where(m => m.FirstDate >= DateTime.Now);
            DateTime? min = date.Min(dt=> (DateTime?)dt.FirstDate);
            var data = db.Tour.Where(m => m.DateTour.Any(dt => dt.FirstDate == min)).FirstOrDefault();
            return data;
        }
        protected List<Tour> TourList(int tourStatusId, int typeofTourId)
        {
            var date = db.Tour.Include(m => m.DateTour)
               .Where(m => m.TourStatusId == tourStatusId && m.TypeOfTourId == typeofTourId)
               .SelectMany(m => m.DateTour)
               .Where(m => m.FirstDate >= DateTime.Now)
               .OrderBy(m=>m.FirstDate).ToList();
            List<Tour> data  = new List<Tour>(); 
           foreach(var Item in date)
           {
               var dataItem = db.Tour.Where(m => m.DateTour.Any(dt => dt.FirstDate == Item.FirstDate) && m.TourStatusId == tourStatusId && m.TypeOfTourId == typeofTourId).First();
               data.Add(dataItem);
           }
           data = data.ToList();
            //  DateTime? min = date.Min(dt=> (DateTime?)dt.FirstDate);
           // var data = db.Tour.Where(m => m.DateTour.Any(dt => dt.FirstDate >= min) && m.TourStatusId == tourStatusId && m.TypeOfTourId == typeofTourId)
             //   .Include(m=>m.DateTour)
             //   .ToList();
            return data;
        }
        public ActionResult Index()
        {
            var data = new TourForHomePage();
            data.TourHightPrev = TourBlock(5, 1);
            data.TourHight = TourList(1, 1);
            data.TourCenterPrev = TourBlock(5, 2);
            data.TourCenter = TourList(1, 2);
            data.TourDownPrev = TourBlock(5, 3);
            data.TourDown = TourList(1, 3);
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
