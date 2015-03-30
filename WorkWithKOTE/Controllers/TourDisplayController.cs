using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
namespace WorkWithKOTE.Controllers
{
    public class TourDisplayController : Controller
    {
        //
        // GET: /TourDisplay/
        UsersContext db = new UsersContext();
        public ActionResult Index(int id = 0)
        {
            if (id != 0)
            {
                var data = new TourForDisplaing();
                data.TourForDisplay = db.Tour.Find(id);
                data.DateForDisplay = db.DateTours.Where(m => m.TourId == id).ToList();
                data.DopUslugForDisplay = db.DopUslugs.Where(m => m.TourId == id).ToList();
                data.TagForDisplay = db.Teg.Where(m => m.TourId == id).ToList();
                return View(data);
            }
            return View();//Добавить страницу ошибки
        }
        public ActionResult DatePartial(int id)
        {

            var date = db.DateTours.Where(m => m.TourId == id);

            return PartialView(date);
        }
        public ActionResult DopUsluga(int id)
        {

            var date = db.DopUslugs.Where(m => m.TourId == id);

            return PartialView(date);
        }
        public ActionResult Teg(int id)
        {

            var date = db.Teg.Where(m => m.TourId == id);
            string file_path = Server.MapPath("~/Files/PDFIcon.pdf");
            return PartialView(date);
        }
        public ActionResult FileDownload(int id)
        {
            var data = db.Tour.Find(id);
            string file_path = data.Document;
            string file_name = Path.GetFileName(file_path);
            string extension = "application/octet-stream";
            if (file_name != null)
                return File(file_path, extension, file_name);
            else
                return RedirectToAction("Index", id);
        }
        public ActionResult MapForDisplay(int id)
        {
            var data = db.RoutePoint.Where(m => m.TourId == id);
            return PartialView(data);
        }
        public ActionResult TourDelete(int id = 0)
        {
            if(id != 0)
            {
                var model = db.Tour.Find(id);
                var date = db.DateTours.Where(m => m.TourId == id);
                var Tag = db.Teg.Where(m => m.TourId == id);
                var Dopuslg = db.DopUslugs.Where(m=>m.TourId == id);
                var MapPoint = db.RoutePoint.Where(m => m.TourId == id);
                var Trip = db.Trip.Where(m=>m.TourId == id);
                db.Entry(model).State = System.Data.EntityState.Deleted;
                foreach (var item in date) { db.Entry(item).State = System.Data.EntityState.Deleted;}
                foreach (var item in date) { db.Entry(item).State = System.Data.EntityState.Deleted; }
                foreach (var item in date) { db.Entry(item).State = System.Data.EntityState.Deleted; }
                foreach (var item in date) { db.Entry(item).State = System.Data.EntityState.Deleted; }
                foreach (var item in date) { db.Entry(item).State = System.Data.EntityState.Deleted; }
                db.SaveChanges();
            }
            return RedirectToAction("Index","Home");
        }
    }
}
