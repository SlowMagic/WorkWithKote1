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
        public ActionResult ForWait()
        {
            return View();
        }
        protected Tour TourBlock(int tourStatusId,int typeofTourId)
        {
            var date = db.Tour.Include(m => m.DateTour)
                .Where(m => m.TourStatusId == tourStatusId && m.TypeOfTourId == typeofTourId)
                .SelectMany(m => m.DateTour)
                .Where(m => m.FirstDate >= DateTime.Now);
            DateTime? min = date.Min(dt=> (DateTime?)dt.FirstDate);
            var data = db.Tour.Include(m => m.BigLogo).Where(m => m.DateTour.Any(dt => dt.FirstDate == min) && (m.TourStatusId == tourStatusId && m.TypeOfTourId == typeofTourId)).FirstOrDefault();
            return data;
        }
        protected List<Tour> TourList(int tourStatusId, int typeofTourId)
        {
            List<DateTour> date = new List<DateTour>();
            if(typeofTourId != 0){
               date = db.Tour.Include(m => m.DateTour)
               .Where(m => m.TourStatusId == tourStatusId)
               .SelectMany(m => m.DateTour)
               .Where(m => m.FirstDate >= DateTime.Now)
               .OrderBy(m=>m.FirstDate).ToList();

            }
            else
            {
                date = db.Tour.Include(m => m.DateTour)
               .Where(m => m.TourStatusId == tourStatusId && m.TypeOfTourId == typeofTourId)
               .SelectMany(m => m.DateTour)
               .Where(m => m.FirstDate >= DateTime.Now)
               .OrderBy(m=>m.FirstDate).ToList();
            }
            List<Tour> data = new List<Tour>();
            List<Tour> data1 = new List<Tour>();
           if(typeofTourId != 0)
           foreach(var Item in date)
           {
               var dataItem = db.Tour.Where(m => m.DateTour.Any(dt => dt.FirstDate == Item.FirstDate) && m.TourStatusId == tourStatusId ).First();
               data.Add(dataItem);
           }
           else
               foreach (var Item in date)
               {
                   var dataItem = db.Tour.Where(m => m.DateTour.Any(dt => dt.FirstDate == Item.FirstDate) && m.TourStatusId == tourStatusId && m.TypeOfTourId == typeofTourId).First();
                   data.Add(dataItem);
               }

           if(data.Count != 0)
           for (int i = 0; i < data.Count; i++ )
           {
               int k = data.Last().TourId;
               if (data[i].TourId != k)
               {
                   if (data[i].TourId != data[i+1].TourId)
                       data1.Add(data[i]);
               }
               else if(data[i].TourId != data[0].TourId)
                   data1.Add(data[i]);
               else if(i == 0)
                   data1.Add(data[i]);
           }
               return data1;
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
            data.TourMain = TourList(4, 0);
            return View(data);
        }
        public ActionResult DateForCurrentTour(int id)
        {
            
            var date = db.DateTours.Where(m => m.TourId== id);
          
            return PartialView(date);
        }
        public ActionResult NewsBlock()
        {
            var data = db.News.Where(m => m.NewsDate.Month >= DateTime.Now.Month).OrderBy(m => m.NewsDate).ToList();
            return PartialView(data);
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
