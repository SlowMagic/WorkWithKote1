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
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
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
                data.Valuta = tour.Valuta;
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
            var tour = db.Tour.Find(id);
            decimal Price = tour.Cost.Value;
            if(Item != null)
            for (int i = 0; i < Item.Length; i++)
            {
                var DopUslug = db.DopUslugs.Find(Item[i]);
                Price += DopUslug.Price;
            }
            if (model.TourPrice == Price)
            {
                model.Status = "Не оплачена";
                model.DateTourId = int.Parse(DateTourId);
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
                if (!Request.IsAuthenticated)
                {
                    int anonimusID = WebSecurity.GetUserId("Anonimus@mail.com");
                    UserProfile anonimus = db.UserProfiles.Find(anonimusID);
                    anonimus.Trips = new List<Trip>();
                    anonimus.Trips.Add(model);
                    db.Entry(anonimus).State = EntityState.Added;
                }
                db.Entry(tour).State = EntityState.Modified;
                db.Entry(model).State = EntityState.Added;
                db.SaveChanges();
                return RedirectToAction("Payment", new { id = model.TripID});
            }
            else
                return RedirectToAction("Error","Error");
        }
        public ActionResult DopPricePartial(int id = 0)
        {
            var data = db.DopUslugs.Where(m=>m.TourId == id);
            
            return View(data);
        }
       public ActionResult Payment(int id)
        {
            var data = db.Trip.Find(id);
            var dataTour = db.Tour.Find(data.TourId);

            var dataObj = new
            {
                version = 3,
                public_key ="i69833650669",
                amount = data.TourPrice,
                currency = data.Valuta,
                description = dataTour.NameTour,
                order_id = 5,//data.TripID,
                pay_way = "card,liqpay,delayed,invoice,privat24",
                language = "ru"
            };

            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            string dataJson = serializer.Serialize(dataObj);
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(dataJson);
            string dataStr = System.Convert.ToBase64String(bytes);
            string privateKey = "v2Rhrz287rrJtDSHq228gsg70ZkA8omC2mhl7aha";
            var signature = privateKey + dataStr + privateKey;

            SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(signature));

            ViewBag.Data = dataStr;
            ViewBag.Signature = System.Convert.ToBase64String(hash);

            return View();
        }
         [HttpPost]
       public ActionResult ValidatePay(string data, string signature)
       {
           string privateKey = "v2Rhrz287rrJtDSHq228gsg70ZkA8omC2mhl7aha";
            byte[] dataByte = Convert.FromBase64String(data);
            string dataStr = Encoding.UTF8.GetString(dataByte);
            TripJS obj = JsonConvert.DeserializeObject<TripJS>(dataStr);
            int TripId = obj.order_id;
           data = privateKey + data + privateKey;
           SHA1 sha1 = SHA1.Create();
            byte[] hash = sha1.ComputeHash(System.Text.Encoding.UTF8.GetBytes(data));
             data = System.Convert.ToBase64String(hash);
             if(string.Compare(data,signature) == 0)
             {
                 var Trip = db.Trip.Find(TripId);
                 var tour = db.Tour.Find(Trip.TourId);
                 var dateTour = db.DateTours.Find(Trip.DateTourId);
                 if(Trip.UserId != 1)
                 Trip.Status = "Оплачена";
                 VisitedTour addtour = new VisitedTour();
                 addtour.TourName = tour.NameTour;
                 addtour.FirstDate = dateTour.FirstDate;
                 addtour.SecondDate = dateTour.SecondDate;
                 var userprofile = db.UserProfiles.Find(Trip.UserId);
                 userprofile.VisitedTours = new List<VisitedTour>();
                 userprofile.VisitedTours.Add(addtour);
                 db.Entry(Trip).State = EntityState.Modified;
                 db.Entry(userprofile).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index", "TourDisplay", new { id = Trip.TourId});
             }
           return View("Error","Error");
       }
        
    }
}
