using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
using System.Data;
using System.Text.RegularExpressions;
using System.IO;
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
            return View();

        }
        [HttpPost]

        public ActionResult TourCreate(Tour model, HttpPostedFileBase TourImg, HttpPostedFileBase Document, HttpPostedFileBase AvatarSupp)
        {
            string check = UploadImg(TourImg, "/UpLoad/TourImg/");

            if (check != null)
                model.TourImg = check;
            check = UploadImg(AvatarSupp, "/UpLoad/SuppFoto/");
            if (check != null)
                model.SuppFoto = check;
            if (Document != null)
            {
                Document.SaveAs(Server.MapPath("/UpLoad/TourDocument/" + Document.FileName));
                model.Document = "/UpLoad/TourDocument/" + Document.FileName;
            }
            model.DescriptionTour = Regex.Replace(model.DescriptionTour, "<script.*?</script>", "", RegexOptions.IgnoreCase);
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Search", "TourSearch");
        }
        public ActionResult TourEdit(int id = 0)
        {
            var data = new TourForEdit();
            data.MyTour = db.Tour.Find(id);
            data.TourDate = db.DateTours.Where(m => m.TourId == id).ToList();
            data.TourDopUsluga = db.DopUslugs.Where(m => m.TourId == id).ToList();
            data.TourTag = db.Teg.Where(m => m.TourId == id).ToList();
            data.RoutPointTour = db.RoutePoint.Where(m => m.TourId == id).ToList();
            ViewBag.GalleryID = new SelectList(db.Gallery, "GalleryId", "GalleryName");
            return View(data);
        }
        [HttpPost]
        public ActionResult TourEdit(TourForEdit model, HttpPostedFileBase TourImg, HttpPostedFileBase Document, HttpPostedFileBase AvatarSupp)
        {
            if(model.RoutPointTour == null)
            {
                foreach (var item in model.RoutPointTour)
                {
                    db.RoutePoint.Remove(item);
                }
            }
            if(model.RoutPoints !=null)
                foreach(var item in model.RoutPoints)
                {
                    item.TourId = model.MyTour.TourId;
                    db.Entry(item).State = EntityState.Added;
                }
            if (model.TourDate != null)
                foreach (var item in model.TourDate)
                {
                    item.TourId = model.MyTour.TourId;
                    db.Entry(item).State = EntityState.Modified;
                }
            if (model.TourTag != null)
                foreach (var item in model.TourTag)
                {
                    item.TourId = model.MyTour.TourId;
                    db.Entry(item).State = EntityState.Modified;
                }
            if (model.TourDopUsluga != null)
                foreach (var item in model.TourDopUsluga)
                {
                    item.TourId = model.MyTour.TourId;
                    db.Entry(item).State = EntityState.Modified;
                }
            if (model.Date != null)
                foreach (var item in model.Date)
                {
                    db.Entry(item).State = EntityState.Added;
                }
            if (model.DopU != null)
                foreach (var item in model.DopU)
                {
                    db.Entry(item).State = EntityState.Added;
                }
            if (model.TagTour != null)
                foreach (var item in model.TagTour)
                {
                    db.Entry(item).State = EntityState.Added;
                }
            string check = UploadImg(TourImg, "/UpLoad/TourImg/");
            if (check != null)
                model.MyTour.TourImg = check;
            check = UploadImg(AvatarSupp, "/UpLoad/SuppFoto/");
            if (check != null)
                model.MyTour.SuppFoto = check;
            model.MyTour.DescriptionTour = Regex.Replace(model.MyTour.DescriptionTour, "<script.*?</script>", "", RegexOptions.IgnoreCase);
            if (Document != null)
            {
                Document.SaveAs(Server.MapPath("/UpLoad/TourDocument/" + Document.FileName));
                model.MyTour.Document = "/UpLoad/TourDocument/" + Document.FileName;
            }
            db.Entry(model.MyTour).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        protected string UploadImg(HttpPostedFileBase file, string path)
        {
            if (file != null)
            {
                string fullPath = null;
                string file1 = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(file.FileName);
                file1 += extension;
                List<string> extensions = new List<string>() { ".png", ".jpg", ".gif" };
                if (extensions.Contains(extension))
                {
                    file.SaveAs(Server.MapPath(path + file1));
                    fullPath = path + file1;
                }
                return fullPath;
            }
            return null;
        }

    }
}
