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
        TourContext db = new TourContext();
        public ActionResult TourCreate()
        {
             ViewBag.GalleryID = new SelectList(db.Gallery, "GalleryId", "GalleryName");
             return View();

        }
        [HttpPost]
        public ActionResult TourCreate(Tour model, HttpPostedFileBase TourImg, HttpPostedFileBase Document, HttpPostedFileBase AvatarSupp)
        {
            if (TourImg != null)
            {
                string file1 = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(TourImg.FileName);
                file1 += extension;
                List<string> extensions = new List<string>() { ".png", ".jpg", ".gif" };
                if (extensions.Contains(extension))
                {
                    TourImg.SaveAs(Server.MapPath("/UpLoad/TourImg/" + file1));
                    model.TourImg = "/UpLoad/TourImg/" + file1;
                }
            }
            if (AvatarSupp != null)
            {
                string file2 = Guid.NewGuid().ToString();
                string extension1 = Path.GetExtension(AvatarSupp.FileName);
                file2 += extension1;
                List<string> extensions = new List<string>() { ".png", ".jpg", ".gif" };
                if (extensions.Contains(extension1))
                {
                    AvatarSupp.SaveAs(Server.MapPath("/UpLoad/SuppFoto/" + file2));
                    model.SuppFoto = "/UpLoad/SuppFoto/" + file2;
                }
            }
             if(Document !=null){
            Document.SaveAs(Server.MapPath("/UpLoad/TourDocument/"+Document.FileName));
            model.Document = "/UpLoad/TourDocument/"+Document.FileName;
            }
            model.DescriptionTour = Regex.Replace(model.DescriptionTour, "<script.*?</script>", "", RegexOptions.IgnoreCase);
            db.Entry(model).State = EntityState.Added;
            db.SaveChanges();
            return RedirectToAction("Search","TourSearch");
        }
        public ViewResult EmptyDateTour()
        {
            return View("PartialDateTour", new DateTour());
        }

    }
}
