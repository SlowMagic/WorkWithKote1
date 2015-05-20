using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
using System.Data.Entity;
namespace WorkWithKOTE.Controllers
{
    public class TourSearchController : Controller
    {
        //
        // GET: /TourSearch/
        // TourContext db = new TourContext();
        UsersContext db = new UsersContext();
        public ActionResult Search()
        {
            return View(db.Tour.Include(m => m.DateTour).ToList());
        }

        [HttpPost]
        public ActionResult Search(String Tag, String Date = null, String TypeOfTour = null, String ArrivePlace = null, bool IsCar = false, bool IsShip = false, bool IsTrain = false, bool IsPlane = false, string Prices = null)
        {
            IQueryable<Tour> data = db.Tour;
            if (!String.IsNullOrEmpty(Date))
            {
                int month = int.Parse(Date);
                data = data.Where(m => m.DateTour.Any(dt => dt.FirstDate.Month == month));
            }
            if (!String.IsNullOrEmpty(TypeOfTour))
            {
                int typeTourId = int.Parse(TypeOfTour);
                data = data.Where(m => m.TypeOfTourId == typeTourId);
            }
            if (!String.IsNullOrEmpty(ArrivePlace))
            {
                data = data.Where(m => m.PlaceOfArrival == ArrivePlace);
            }
            if (!String.IsNullOrEmpty(Tag))
            {
                string[] separator = { ",", ".", ";", " " };
                string[] TagMass = Tag.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                foreach (var item in TagMass)
                {
                    data = data.Where(m => m.Tag.Any(t => t.TagName == item));
                }
            }
            if (IsCar == true)
                data = data.Where(m => m.IsBus == true);
            if (IsPlane == true)
                data = data.Where(m => m.IsAriplane == true);
            if (IsShip == true)
                data = data.Where(m => m.IsShip == true);
            if (IsTrain == true)
                data = data.Where(m => m.IsTrain == true);
            if (!String.IsNullOrEmpty(Prices))
            {
                decimal price = decimal.Parse(Prices);
                data = data.Where(m => m.Cost >= price);
            }
            return View(data.Include(m => m.DateTour));
        }
        public ActionResult Searches()
        {
            ViewBag.Counter = 0;
            return View(db.Tour.Include(m => m.DateTour).Include(m=>m.Tag).ToList());
        }
        [HttpPost]
        public ActionResult Searches( string Places, DateTime? DateFirst, DateTime? DateSecond,decimal? MinValue,decimal? MaxValue, bool IsCar = false, bool IsShip = false, bool IsTrain = false, bool IsPlane = false,string[] Tags = null)
        {
            ViewBag.Counter = 1;
            IQueryable<Tour> data = db.Tour;
            if (!String.IsNullOrEmpty(Places))
            {
                ViewBag.Place = Places;
                data = data.Where(m => m.PlaceOfArrival.Contains(Places) || m.Places.Any(k => k.PlaceName.Contains(Places)));
            }
            if (DateFirst != null)
            {
                ViewBag.DateFirs = DateFirst.Value.ToString("yyyy-MM-dd");
                DateTime dateMin = DateFirst.Value.AddDays(-5);
                DateTime dateMax = DateFirst.Value.AddDays(5);
                data = data.Where(m => m.DateTour.Any(dt => dt.FirstDate >= dateMin && dt.FirstDate <= dateMax));
            }
            if(DateSecond != null)
            {
                ViewBag.DateSecond = DateSecond.Value.ToString("yyyy-MM-dd");
                DateTime dateMin = DateSecond.Value.AddDays(-5);
                DateTime dateMax = DateSecond.Value.AddDays(5);
                data = data.Where(m => m.DateTour.Any(dt => dt.SecondDate >= dateMin && dt.SecondDate <= dateMax));
            }
            if(MinValue != null)
            {
                ViewBag.MinValue = MinValue;
                data = data.Where(m => m.Cost >= MinValue || m.AukcionPrice >= MinValue);
            }
            if(MaxValue != null)
            {
                ViewBag.MaxValue = MaxValue;
                data = data.Where(m => m.Cost <= MaxValue || m.AukcionPrice <= MaxValue);
            }
            if (IsCar == true)
                data = data.Where(m => m.IsBus == true);
            if (IsPlane == true)
                data = data.Where(m => m.IsAriplane == true);
            if (IsShip == true)
                data = data.Where(m => m.IsShip == true);
            if (IsTrain == true)
                data = data.Where(m => m.IsTrain == true);
            if (Tags != null)
            {
                foreach (var item in Tags)
                {
                    data = data.Where(m => m.Tag.Any(tg => tg.TagName == item));
                }
            }
            ViewBag.Tags = data.SelectMany(m => m.Tag).Select(m => m.TagName).Distinct();
            var places = data.Select(m => m.PlaceOfDeparture).Distinct();
            List<System.Web.Mvc.SelectListItem> list = new List<SelectListItem>();
            foreach (var item in places)
            {
                list.Add(new SelectListItem { Text = item });
            }
            ViewBag.PlaceDeparture = list;
            return View(data.Include(m=>m.DateTour));
        }
    }
}

