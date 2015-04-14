using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using System.Data.Entity;
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
            Trip data = new Trip();
            Tour tour = db.Tour.Find(id);
            if (tour != null)
            {
                ViewBag.TourPrices = tour.Cost;
                data .TourId = id;
                data.TourPrice = tour.Cost;
                ViewBag.DateTourId = new SelectList(db.DateTours.Where(m => m.TourId == id)
                .AsEnumerable()
                .Select(m => new
                {
                    Date = m.FirstDate.ToString("dd.MM.yyyy"),
                    ID = m.DateTourId
                }), "ID", "Date");
                data.AmtPeople = 1;
                if (Request.IsAuthenticated)
                {
                    int userID = WebSecurity.GetUserId(User.Identity.Name);
                    var userprofile = db.UserProfiles.Find(userID);
                    data.email = userprofile.Email;
                    data.BirthDay = userprofile.Birthday;
                    data.Citizenship = userprofile.Nationality;
                    data.Name = userprofile.RuFirstName;
                    data.Surname = userprofile.RuSecondName;
                    data.FatherName = userprofile.RuThirdName;
                    data.mobile = userprofile.Mobile;
                    data.Pasport = userprofile.PasportData;
                }
                ViewBag.TitleOf = tour.NameTour;
            }
            return View(data);
        }
        [HttpPost]
        public ActionResult Index(int id,Trip model,string DateTourId , int[] Item)
        {
            VisitedTour addtour = new VisitedTour();
            var tour = db.Tour.Find(id);
            decimal Price = tour.Cost.Value;
            for (int i = 0; i < Item.Length; i++)
            {
                var DopUslug = db.DopUslugs.Find(Item[i]);
                Price += DopUslug.Price;
            }
                model.DateTourId = int.Parse(DateTourId);
            if (Request.IsAuthenticated)
            {
                var dateTour = db.DateTours.Find(model.DateTourId);
                int userID = WebSecurity.GetUserId(User.Identity.Name);
                UserProfile userprofile = db.UserProfiles.Find(userID);
                userprofile.Trips = new List<Trip>();
                userprofile.Trips.Add(model);
                addtour.TourName = tour.NameTour;
                addtour.FirstDate = dateTour.FirstDate;
                addtour.SecondDate = dateTour.SecondDate;
                userprofile.VisitedTours = new List<VisitedTour>();
                userprofile.VisitedTours.Add(addtour);
                db.Entry(userprofile).State = EntityState.Modified;
            }
            tour.Trips = new List<Trip>();
            tour.Trips.Add(model);
            db.Entry(tour).State = EntityState.Modified;
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
