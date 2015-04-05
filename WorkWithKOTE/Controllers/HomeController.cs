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
        protected DateTime LowDate(string s,string k)
        {
            var data = db.Tour.Where(m => m.TypeOfTour == s && m.TourStatus == k).ToArray();
            var date = new DateTime();
            List<int> massId = new List<int>(); 
            foreach(var item in data)
            {
               int j = item.TourId;
               massId.Add(j);
            }
            List<DateTour> AllDateTour = new List<DateTour>();
            foreach(int item in massId){
                var DateTour = db.DateTours.Where(m => m.TourId == item).ToList();
                AllDateTour.AddRange(DateTour);
            }
            for (int i = 0; i <AllDateTour.Count; i++)
            {
                var last = AllDateTour.Last();
                if (AllDateTour[i].DateTourId != last.DateTourId)
                {
                    if (AllDateTour[i].FirstDate <= AllDateTour[i + 1].FirstDate && AllDateTour[i].FirstDate >= DateTime.Now)
                    {
                        if(AllDateTour[i].FirstDate <= date)
                        date = AllDateTour[i].FirstDate;
                    }
                    else
                        date = AllDateTour[i + 1].FirstDate;
                }                 
                else if (AllDateTour[i].FirstDate <= AllDateTour[0].FirstDate && AllDateTour[i].FirstDate >= DateTime.Now)
                    date = AllDateTour[i].FirstDate;
            }
            return date;
        }
        public ActionResult Index()
        {
            var data = new TourForHomePage();
            var neeDate = LowDate("Туры по Украине", "Превью блока");
            data.TourHightPrev = db.Tour.Where(m => m.DateTour.Any(k => k.FirstDate == neeDate) && m.TourStatus == "Превью блока" && m.TypeOfTour == "Туры по Украине").FirstOrDefault();
            neeDate = LowDate("Туры в Грузию", "Превью блока");
            data.TourCenterPrev = db.Tour.Where(m => m.DateTour.Any(k => k.FirstDate == neeDate) && m.TourStatus == "Превью блока" && m.TypeOfTour == "Туры в Грузию").FirstOrDefault();
            neeDate = LowDate("Приключенческие туры", "Превью блока");
            data.TourDownPrev = db.Tour.Where(m => m.DateTour.Any(k => k.FirstDate == neeDate) && m.TourStatus == "Превью блока" && m.TypeOfTour == "Приключенческие туры").FirstOrDefault();
            neeDate = LowDate("Туры по Украине", "Активный");
            data.TourHight = db.Tour.Where(m => m.DateTour.Any(k => k.FirstDate >= neeDate) && m.TourStatus == "Активный" && m.TypeOfTour == "Туры по Украине").ToList();
            neeDate = LowDate("Туры в Грузию", "Активный");
            data.TourCenter = db.Tour.Where(m => m.DateTour.Any(k => k.FirstDate >= neeDate) && m.TourStatus == "Активный" && m.TypeOfTour == "Туры в Грузию").ToList();
            neeDate = LowDate("Приключенческие туры", "Активный");
            data.TourDown = db.Tour.Where(m => m.DateTour.Any(k => k.FirstDate >= neeDate) && m.TourStatus == "Активный" && m.TypeOfTour == "Приключенческие туры").ToList();
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
