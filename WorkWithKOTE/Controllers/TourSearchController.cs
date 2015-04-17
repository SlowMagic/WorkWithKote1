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
            return View(db.Tour.Include(m=>m.DateTour).ToList());
        }

        [HttpPost]
        public ActionResult Search(String Date = null, String TypeOfTour = null, String ArrivePlace = null, bool IsCar = false, bool IsShip = false, bool IsTrain = false, bool IsPlane = false, string Prices = null)
        {
            IQueryable<Tour> data = db.Tour;
            if(!String.IsNullOrEmpty(Date))
            {
                int month = int.Parse(Date);
                    data = data.Where(m=>m.DateTour.Any(dt=>dt.FirstDate.Month == month));
            }
            if(!String.IsNullOrEmpty(TypeOfTour))
            {              
                int typeTourId = int.Parse(TypeOfTour);
                data = data.Where(m => m.TypeOfTourId == typeTourId);
            }
            if(!String.IsNullOrEmpty(ArrivePlace))
            {
                data = data.Where(m=>m.PlaceOfArrival == ArrivePlace);
            }
            if (IsCar == true)
                data = data.Where(m => m.IsBus == true);
            if (IsPlane == true)
                data = data.Where(m=>m.IsAriplane == true);
            if (IsShip == true)
                data = data.Where(m=>m.IsShip == true);
            if (IsTrain == true)
                data = data.Where(m=>m.IsTrain == true);
            if (!String.IsNullOrEmpty(Prices))
            {
                decimal price = decimal.Parse(Prices);
                data = data.Where(m => m.Cost >= price);
            }
            return View(data.Include(m=>m.DateTour));
        }
    }
}
