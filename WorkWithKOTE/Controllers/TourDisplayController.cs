using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
using System.Data.Entity;
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
                var data = db.Tour.Include(m=>m.DopUslug).Include(m => m.DateTour).Include(m => m.RoutePoints).Include(m => m.Tag).Include(m => m.Trips).Include(m=>m.SameTour).Where(m => m.TourId == id).FirstOrDefault();
                    return View(data);
            }
            return View();//Добавить страницу ошибки
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
            try
            {
                var model = db.Tour.Find(id);
                db.Tour.Remove(model);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
