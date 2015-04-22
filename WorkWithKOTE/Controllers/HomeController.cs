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
        protected List<Tour> TourList(int TourStatusId)
        {
            var date = db.Tour.Include(m => m.DateTour)
               .Where(m => m.TourStatusId == TourStatusId)
               .SelectMany(m => m.DateTour)
               .Where(m => m.FirstDate >= DateTime.Now)
               .OrderBy(m => m.FirstDate).ToList();
            List<Tour> data = new List<Tour>();
            foreach (var Item in date)
            {
                var dataItem = db.Tour.Include(m => m.BigLogo).Where(m => m.DateTour.Any(dt => dt.FirstDate == Item.FirstDate) && m.TourStatusId == TourStatusId).ToList();
                foreach (var item in dataItem)
                {
                    data.Add(item);
                }
            }
            var data1 = data.Select(m => m.TourId).Distinct();
            data = new List<Tour>();
            foreach (var item in data1)
            {
                var dataItem = db.Tour.Find(item);
                data.Add(dataItem);
            }
            return data;
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
               var date = db.Tour.Include(m => m.DateTour)
               .Where(m => m.TourStatusId == tourStatusId && m.TypeOfTourId == typeofTourId)
               .SelectMany(m => m.DateTour)
               .Where(m => m.FirstDate >= DateTime.Now)
               .OrderBy(m=>m.FirstDate).ToList();
           List<Tour> data = new List<Tour>();
           foreach(var Item in date)
           {
               var dataItem = db.Tour.Where(m => m.DateTour.Any(dt => dt.FirstDate == Item.FirstDate) && m.TourStatusId == tourStatusId && m.TypeOfTourId == typeofTourId).ToList();
               foreach(var item in dataItem)
               {
                   data.Add(item);
               }
           }
           var data1 = data.Select(m => m.TourId).Distinct();
            data = new List<Tour>();
            foreach(var item in data1)
            {
                var dataItem = db.Tour.Find(item);
                data.Add(dataItem);             
            }
            return data;

        }
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Index()
        {
            var data = new TourForHomePage();
            data.TourHightPrev = TourBlock(5, 1);
            data.TourHight = TourList(1, 1);
            data.TourCenterPrev = TourBlock(5, 2);
            data.TourCenter = TourList(1, 2);
            data.TourDownPrev = TourBlock(5, 3);
            data.TourDown = TourList(1, 3);
            data.TourMain = TourList(4);
            data.Galleries = db.Gallery.Include(m => m.Pics).ToList();
            data.Galleries.Reverse();
            data.TourRecomendHight = TourList(3, 1);
            data.TourRecomendCenter = TourList(3, 2);
            data.TourRecomendDown = TourList(3, 3);
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
            data.Reverse();
            return PartialView(data);
        }
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            
            return View();
        }
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
