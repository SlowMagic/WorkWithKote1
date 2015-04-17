using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
using System.Data.Entity;
using WorkWithKOTE.Code;
namespace WorkWithKOTE.Controllers
{
    public class TourCreateController : Controller
    {
        //
        // GET: /TourCreate/
        //TourContext db = new TourContext();
        UsersContext db = new UsersContext();
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult TourCreate()
        {
            ViewBag.GalleryID = new SelectList(db.Gallery, "GalleryId", "GalleryName");
            ViewBag.TourStatusId = new SelectList(db.TourStatus, "TourStatusId", "TourStatusName");
            ViewBag.TypeOfTourId = new SelectList(db.TypeOfTours, "TypeOfTourId", "TypeOfTourName");
            ViewBag.SameTourId = new MultiSelectList(db.Tour, "TourId", "NameTour");
            ViewBag.LogoId = new SelectList(db.BigLogos,"LogoId","LogoName");
            return View();

        }
        [HttpPost]

        public ActionResult TourCreate(List<int>SameTours,Tour model, HttpPostedFileBase TourImg, HttpPostedFileBase Document, HttpPostedFileBase AvatarSupp)
        {
            if (SameTours != null)
            {
                model.SameTour = new List<SameTour>();
                foreach (int item in SameTours)
                {
                    var d = db.Tour.Find(item);
                    model.SameTour.Add(new SameTour { SameTourID = d.TourId, SameTourName = d.NameTour });
                }
            }
            string check = UploadImages.UploadImg(TourImg, "/UpLoad/TourImg/");

            if (check != null)
                model.TourImg = check;
            check = UploadImages.UploadImg(AvatarSupp, "/UpLoad/SuppFoto/");
            if (check != null)
                model.SuppFoto = check;
            if (Document != null)
            {
                Document.SaveAs(Server.MapPath("/UpLoad/TourDocument/" + Document.FileName));
                model.Document = "/UpLoad/TourDocument/" + Document.FileName;
            }
            if (model.DescriptionTour == null)
                model.DescriptionTour = "<p>Нет описания тура</p>";
            model.DescriptionTour = Regex.Replace(model.DescriptionTour, "<script.*?</script>", "", RegexOptions.IgnoreCase);
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Search", "TourSearch");
        }
        public ActionResult TourEdit(int id = 0)
        {
            var data = db.Tour.Include(m=>m.Trips).Include(m => m.DopUslug).Include(m => m.DateTour).Include(m => m.RoutePoints).Include(m=>m.Tag).Where(m => m.TourId == id).FirstOrDefault();
            ViewBag.TourStatusId = new SelectList(db.TourStatus, "TourStatusId", "TourStatusName",data.TourStatusId);
            ViewBag.GalleryID = new SelectList(db.Gallery, "GalleryId", "GalleryName");
            ViewBag.TypeOfTourId = new SelectList(db.TypeOfTours, "TypeOfTourId", "TypeOfTourName",data.TypeOfTourId);
            ViewBag.SameTourId = new MultiSelectList(db.Tour, "TourId", "NameTour");
            ViewBag.LogoId = new SelectList(db.BigLogos, "LogoId", "LogoName",data.LogoId);
            ViewBag.Id = id;
            return View(data);
        }
        [HttpPost]
        public ActionResult TourEdit(List<int> SameTours, Tour model, HttpPostedFileBase TourImg, HttpPostedFileBase Document, HttpPostedFileBase AvatarSupp, DateTour[] DT, DopUslug[] DP,Tag[] Ts,RoutePoint[] Point)
        {
            if (SameTours != null)
            {
                model.SameTour = new List<SameTour>();
                foreach (int item in SameTours)
                {
                    var d = db.Tour.Find(item);
                    model.SameTour.Add(new SameTour { SameTourID = d.TourId, SameTourName = d.NameTour });
                }
            }
            string check = UploadImages.UploadImg(TourImg, "/UpLoad/TourImg/"); 
            if (check != null)
                model.TourImg = check;
            check = UploadImages.UploadImg(AvatarSupp, "/UpLoad/SuppFoto/");
            if (check != null)
                model.SuppFoto = check;
            if (model.DescriptionTour != null)
            model.DescriptionTour = Regex.Replace(model.DescriptionTour, "<script.*?</script>", "", RegexOptions.IgnoreCase);
            if (Document != null)
            {
                Document.SaveAs(Server.MapPath("/UpLoad/TourDocument/" + Document.FileName));
                model.Document = "/UpLoad/TourDocument/" + Document.FileName;
            }
            if(DT !=null)
            foreach (var item in DT)
            { db.Entry(item).State = EntityState.Added; }
            if(DP!=null)
            foreach (var item in DP)
            { db.Entry(item).State = EntityState.Added; }
            if(Ts != null)
            foreach (var item in Ts)
            { db.Entry(item).State = EntityState.Added; }
            foreach(var item in model.DateTour)
            {
                db.Entry(item).State = EntityState.Modified;
            }
            foreach (var item in model.DopUslug)
            {
                db.Entry(item).State = EntityState.Modified;
            }
           if(Point != null)
                foreach (var item in Point)
                {
                    db.Entry(item).State = EntityState.Added;
                }
            foreach(var item in model.Tag)
            {
                db.Entry(item).State = EntityState.Modified;
            }
            if(model.RoutePoints != null)
            foreach (var item in model.RoutePoints)
            {
                    db.Entry(item).State = EntityState.Modified;
            }
            int i = model.TourId;
            db.Entry(model).State = EntityState.Modified; 
            if(model.RoutePoints != null)  
            foreach(var item in model.RoutePoints.ToList())
            {
                if (item.Lat == 0.0 && item.Lng == 0.0)
                db.Entry(item).State = EntityState.Deleted;
            }
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }
    }
}
