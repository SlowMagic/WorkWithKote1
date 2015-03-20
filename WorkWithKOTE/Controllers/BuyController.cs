using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
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
      //  TourContext db = new TourContext();
        UsersContext db = new UsersContext();
        public ActionResult Index(int id )
        {
            Tour data = db.Tour.Find(id);
            ViewBag.TourPrices = data.Cost;
            Trip trip = new Trip();
            //  trip.TourId = data.TourId;
            trip.TourPrice = data.Cost;
            ViewBag.DateTourId = new SelectList(db.DateTours.Where(m => m.TourId == id), "DateTourId", "FirstDate");
            if (Request.IsAuthenticated)
            {
                int userID = WebSecurity.GetUserId(User.Identity.Name);
                var userprofile = db.UserProfiles.Find(userID);
                trip.email = userprofile.Email;
                trip.BirthDay = userprofile.Birthday;
                trip.Citizenship = userprofile.Nationality;
                trip.Name = userprofile.RuFirstName;
                trip.Surname = userprofile.RuSecondName;
                trip.FatherName = userprofile.RuThirdName;
                trip.mobile = userprofile.Mobile;
                trip.Pasport = userprofile.PasportData;
            }
            ViewBag.TitleOf = data.NameTour;
            return View(trip);
        }
        [HttpPost]
        public ActionResult Index(int id,Trip model)
        {
            var tour = db.Tour.Find(id);
            if (Request.IsAuthenticated)
            {
                int userID = WebSecurity.GetUserId(User.Identity.Name);
                UserProfile userprofile = db.UserProfiles.Find(userID);
                userprofile.Trips = new List<Trip>();
                userprofile.Trips.Add(model);
                db.Entry(userprofile).State = EntityState.Modified;
            }
            tour.Trips = new List<Trip>();
            tour.Trips.Add(model);
            db.Entry(tour).State = EntityState.Modified;
            db.Entry(model).State = EntityState.Added;
         //   db.SaveChanges();
            try
            {

                db.SaveChanges();
            }
            catch (DbEntityValidationException ex)
                {
                    string s = null;
                    string k = null;
                    foreach (DbEntityValidationResult validationError in ex.EntityValidationErrors)
                    {
                         s = "Object: "+validationError.Entry.Entity.ToString();
                        
                        foreach (DbValidationError err in validationError.ValidationErrors)
                        {
                             k  = err.ErrorMessage + "";
                        }
                    }
                    ViewBag.Error = s;
                    ViewBag.Error1= k;
                }

            return RedirectToAction("Index","Home");
        }
        public ActionResult DopPricePartial(int id = 0)
        {
            var data = db.DopUslugs.Where(m=>m.TourId == id);
            
            return View(data);
        }

    }
}
