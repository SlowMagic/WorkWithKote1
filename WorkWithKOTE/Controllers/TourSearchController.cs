using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
using System.Data.Entity;
using PagedList;
using PagedList.Mvc;
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
        public ActionResult Searches(int page = 0, int ourCounter = 0 )
        {
            if (ourCounter == 0)
            {
                ViewBag.Counter = 0;
                ViewBag.MinValue = 1000;
                ViewBag.MaxValue = 3000;
                int pageNumber = 1;
                if (page != 0)
                    pageNumber = page;
                int pageSize = 1;
                var data = db.Tour.Include(m => m.DateTour).Include(m => m.Tag).ToList();
                return View(data.ToPagedList(pageNumber, pageSize));
            }
            else
            {
               int  pageNumber  = page;
                int PageSize = 1 ;
                return Searches(page = pageNumber  );
            }
           
        }
        [HttpPost]
        public ActionResult Searches(string PlaceDepartmen, string Places, string DateFirst, string DateFirstt, string DateSecond, decimal? MinValue, decimal? MaxValue, bool IsCar = false, bool IsShip = false, bool IsTrain = false, bool IsPlane = false, int page = 0, string[] Tags = null)
        {
            ViewBag.Counter = 1;
            // IQueryable<Tour> data = db.Tour;
            IEnumerable<Tour> data = db.Tour.Include(m => m.DateTour).Include(m => m.Tag).Include(m => m.Places).AsEnumerable();
            var actualRates = db.Curseds.First();
            Dictionary<string, decimal> rates = new Dictionary<string, decimal>();
            rates.Add("UAH", 1);
            rates.Add("USD", actualRates.USD);
            rates.Add("EUR", actualRates.Evro);
            if (!String.IsNullOrEmpty(Places))
            {
                ViewBag.Place = Places;
                data = data.Where(m => m.PlaceOfArrival.Contains(Places) || m.Places.Any(k => k.PlaceName.Contains(Places)));
            }
            if (!String.IsNullOrEmpty(PlaceDepartmen))
            {
                data = data.Where(m => m.PlaceOfDeparture.Contains(PlaceDepartmen));
            }
            if (!String.IsNullOrEmpty(DateFirst))
            {
                int month = int.Parse(DateFirst);
                data = data.Where(m => m.DateTour.Any(dt => dt.FirstDate.Month == month));
            }

            if (!String.IsNullOrEmpty(DateFirstt))
            {
                ViewBag.DateFirst = DateFirstt;
                DateTime dateMin = DateTime.ParseExact(DateFirstt, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(-5);
                DateTime dateMax = DateTime.ParseExact(DateFirstt, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(5);
                data = data.Where(m => m.DateTour.Any(dt => dt.FirstDate >= dateMin && dt.FirstDate <= dateMax));
            }
            if (!String.IsNullOrEmpty(DateSecond))
            {
                ViewBag.DateSecond = DateSecond;
                DateTime dateMin = DateTime.ParseExact(DateSecond, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(-5);
                DateTime dateMax = DateTime.ParseExact(DateSecond, "dd.MM.yyyy", System.Globalization.CultureInfo.InvariantCulture).AddDays(5);
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
            /*  if (PageCount != 0 && PageID != 0)
              {
                  return View(data.Skip(PageCount * PageID).Take(PageCount));
              }
              if (HomeCounter == 1)
              {
                  return View(data);
              }*/
            int pageNumber = 1;
            if (page != 0)
                pageNumber = page;
            int pageSize = 1;
            data = data.ToList();
            return View(data.ToPagedList(pageNumber, pageSize));
        }
    }
}

