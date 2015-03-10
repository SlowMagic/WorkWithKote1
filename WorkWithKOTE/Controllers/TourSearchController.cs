using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
namespace WorkWithKOTE.Controllers
{
    public class TourSearchController : Controller
    {
        //
        // GET: /TourSearch/
        TourContext db = new TourContext();
        public ActionResult Search()
        {
            ViewBag.DateForTours1 = new SelectList(db.DateTours, "DateTourId", "FirstDate");
            ViewBag.DateForTours2 = new SelectList(db.DateTours, "DateTourId", "SecondDate");
            ViewBag.TypeOfTour = new SelectList(db.Tour, "TypeOfTour");
            return View(db.Tour.ToList());
        }
        [HttpPost]
        public ActionResult Search(string DateForTours1 = null, string  DateForTours2 = null, string? Transport, string? TypeOfTour1)
        {

            var data = new Tour();
            if (DateForTours1 != null)
            {
               
            }

            return View();
        }

    }
}
