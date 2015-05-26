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
            ViewBag.MinValue = 1000;
            ViewBag.MaxValue = 3000;
            return View(db.Tour.Include(m => m.DateTour).Include(m => m.Tag).ToList());
        }
        [HttpPost]
        public ActionResult Searches(string PlaceDepartmen, string Places, DateTime? DateFirst, DateTime? DateSecond, decimal? MinValue, decimal? MaxValue, bool IsCar = false, bool IsShip = false, bool IsTrain = false, bool IsPlane = false, int PageID = 0,
            int PageCount =5,int HomeCounter = 0, string[] Tags = null)
        {
            ViewBag.Counter = 1;
           // IQueryable<Tour> data = db.Tour;
            IEnumerable<Tour> data = db.Tour.Include(m => m.DateTour).Include(m=>m.Tag).Include(m=>m.Places).AsEnumerable();
            var actualRates = db.Curseds.First();
            Dictionary<string, decimal> rates = new Dictionary<string, decimal>();
            rates.Add("UAH", 1);
            rates.Add("USD", actualRates.USD);
            rates.Add("EUR", actualRates.Evro);
            if (!String.IsNullOrEmpty(Places))
            {
                ViewBag.Place = Places;
                data = data.Where(m => m.PlaceOfArrival.Contains(Places) || m.Places.Any(k => k.PlaceName.Contains(Places)));
               // data = data.Where(m => m.Places.Any(k => k.PlaceName.Contains(Places)));
            }
            if (!String.IsNullOrEmpty(PlaceDepartmen))
            {
                data = data.Where(m => m.PlaceOfDeparture.Contains(PlaceDepartmen));
            }
            if (DateFirst != null)
            {
                ViewBag.DateFirst = DateFirst.Value.ToString("yyyy-MM-dd");
                DateTime dateMin = DateFirst.Value.AddDays(-5);
                DateTime dateMax = DateFirst.Value.AddDays(5);
                data = data.Where(m => m.DateTour.Any(dt => dt.FirstDate >= dateMin && dt.FirstDate <= dateMax));
            }
            if (DateSecond != null)
            {
                ViewBag.DateSecond = DateSecond.Value.ToString("yyyy-MM-dd");
                DateTime dateMin = DateSecond.Value.AddDays(-5);
                DateTime dateMax = DateSecond.Value.AddDays(5);
                data = data.Where(m => m.DateTour.Any(dt => dt.SecondDate >= dateMin && dt.SecondDate <= dateMax));
            }
            if (MinValue != null)
            {
                ViewBag.MinValue = MinValue;
                data = data.Where(m => (m.Cost * rates[m.Valuta]) >= MinValue || m.AukcionPrice >= MinValue);
            }
            if (MaxValue != null)
            {
                ViewBag.MaxValue = MaxValue;
                data = data.Where(m => (m.Cost * rates[m.Valuta]) <= MaxValue || m.AukcionPrice <= MaxValue);
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
            var places = data.Select(m => m.PlaceOfDeparture).Distinct();

            List<string> list = new List<string>();
            if (places.Any())
            {
                foreach (var item in places)
                {
                    list.Add(item);
                }
            }
            ViewBag.PlaceDeparture = list;
            if (PageCount != 0 && PageID != 0)
            {
                return View(data.Skip(PageCount * PageID).Take(PageCount));
            }
            if (HomeCounter == 1)
            {
                return View(data);
            }
            return View(data);
        }
    }
}

