using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using WorkWithKOTE.Models;
namespace WorkWithKOTE.Controllers
{

    public class BuyController : Controller
    {
        //
        // GET: /Buy/
        TourContext db = new TourContext();
        UsersContext db1 = new UsersContext();
        public ActionResult Index(int id )
        {
            Tour data = db.Tour.Find(id);
            ViewBag.TourPrices = data.Cost;
            Trip trip = new Trip();
            trip.TourId = data.TourId;
            trip.TourPrice = data.Cost;
            ViewBag.DateTourId = new SelectList(db.DateTours.Where(m => m.TourId == id), "DateTourId", "FirstDate");
            if (Request.IsAuthenticated)
            {
                int userID = WebSecurity.GetUserId(User.Identity.Name);
                var userprofile = db1.UserProfiles.Find(userID);
                trip.email = userprofile.Email;
                trip.BirthDay = userprofile.Birthday;
                trip.Citizenship = userprofile.Nationality;
                trip.Name = userprofile.RuFirstName;
                trip.Surname = userprofile.RuSecondName;
                trip.FatherName = userprofile.RuThirdName;
                trip.mobile = userprofile.Mobile;
                trip.Pasport = userprofile.PasportData;
            }
            return View(trip);
        }
        [HttpPost]
        public ActionResult Index(Trip model)
        {
           /* if (Request.IsAuthenticated)
            {
                int userID = WebSecurity.GetUserId(User.Identity.Name);
                UserProfile userprofile = db1.UserProfiles.Find(userID);
                model.Users.Add(userprofile);
            }*/
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
