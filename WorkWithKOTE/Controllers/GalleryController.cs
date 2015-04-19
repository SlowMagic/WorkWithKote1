using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
using WorkWithKOTE.Code;
using System.Data;
using System.Data.Entity;

namespace WorkWithKOTE.Controllers
{
    public class GalleryController : Controller
    {
        //
        // GET: /Gallery/
        [Authorize(Roles = "Admin, Moderator")]
        UsersContext db = new UsersContext();

        public ActionResult CreateGallery()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateGallery(Gallery model, HttpPostedFileBase[] file)
        {
            List<HttpPostedFileBase> Files = new List<HttpPostedFileBase>();
            for (int i = 0; i < file.Length; i++)
            {
                if (file[i] != null)
                   model.Pics[i].PicLink = UploadImages.UploadImg(file[i], "/Upload/Gallery/");

            }
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("GalleryList");
        }
        public ActionResult GalleryList()
        {
            return View(db.Gallery);
        }
        public ActionResult Gallery(int id)
        {
            return View(db.Gallery.Include(m => m.Pics).Where(m => m.GalleryId == id).Single());
        }
        public ActionResult GalleryDelete(int id)
        {
            var gallery = db.Gallery.Find(id);
            db.Gallery.Remove(gallery);
            db.SaveChanges();
            return RedirectToAction("GalleryList");
        }
    }
}
