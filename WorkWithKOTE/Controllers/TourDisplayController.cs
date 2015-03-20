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
       // TourContext db = new TourContext();
        UsersContext db = new UsersContext();
        public ActionResult Index(int id=0)
        {
           if(id != 0){
            var data = db.Tour.Find(id);
            if(data.IsBus)
             ViewBag.Message = "Автобус"; 
            if(data.IsAriplane)
             ViewBag.Message1 = "Авиалинии"; 
            if (data.IsShip)
             ViewBag.Message2 = "Лайнером";

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
                return RedirectToAction("Index",id);
        }
      
    }
}
