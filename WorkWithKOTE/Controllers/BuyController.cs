using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
namespace WorkWithKOTE.Controllers
{

    public class BuyController : Controller
    {
        //
        // GET: /Buy/
        TourContext db = new TourContext();
        public ActionResult Index(int id )
        {
            Tour data = db.Tour.Find(id);
            ViewBag.TourPrices = data.Cost;
            Trip trip = new Trip();
            trip.TourId = data.TourId;
            trip.TourPrice = data.Cost;
            ViewBag.DateTourId = new SelectList(db.DateTours.Where(m => m.TourId == id), "DateTourId", "FirstDate");
            return View(trip);
        }
        [HttpPost]
        public ActionResult Index(Trip model)
        {
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Index","Home");
        }
        public ActionResult DopPricePartial(int id = 0)
        {
            var data = db.DopUslugs.Where(m=>m.TourId == id);
            
            return View(data);
        }

    }
}
