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
            BuyTour data = new BuyTour();
            Tour tour = db.Tour.Find(id);
            if (tour != null)
            {
                data.Uslugi = db.DopUslugs.Where(m => m.TourId == id).ToList();
                ViewBag.TourPrices = tour.Cost;
                data.Trips = new Trip();
                //   data.Trips .TourId = data.TourId;
                data.Trips.TourPrice = tour.Cost;
                ViewBag.DateTourId = new SelectList(db.DateTours.Where(m => m.TourId == id), "DateTourId", "FirstDate");
                if (Request.IsAuthenticated)
                {
                    int userID = WebSecurity.GetUserId(User.Identity.Name);
                    var userprofile = db.UserProfiles.Find(userID);
                    data.Trips.email = userprofile.Email;
                    data.Trips.BirthDay = userprofile.Birthday;
                    data.Trips.Citizenship = userprofile.Nationality;
                    data.Trips.Name = userprofile.RuFirstName;
                    data.Trips.Surname = userprofile.RuSecondName;
                    data.Trips.FatherName = userprofile.RuThirdName;
                    data.Trips.mobile = userprofile.Mobile;
                    data.Trips.Pasport = userprofile.PasportData;
                }
                ViewBag.TitleOf = tour.NameTour;
            }
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(int id,BuyTour model,string DateTourId)
        {
            var tour = db.Tour.Find(id);
            model.Trips.DateTourId = int.Parse(DateTourId);
            if (Request.IsAuthenticated)
            {
                int userID = WebSecurity.GetUserId(User.Identity.Name);
                UserProfile userprofile = db.UserProfiles.Find(userID);
                userprofile.Trips = new List<Trip>();
                userprofile.Trips.Add(model.Trips);
                db.Entry(userprofile).State = EntityState.Modified;
            }
            tour.Trips = new List<Trip>();
            tour.Trips.Add(model.Trips);
            db.Entry(tour).State = EntityState.Modified;
            db.Entry(model.Trips).State = EntityState.Added;
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
