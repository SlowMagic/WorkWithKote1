using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkWithKOTE.Models;
namespace WorkWithKOTE.Controllers
{
    public class TourSearchController : Controller
    {
        //
        // GET: /TourSearch/
        TourContext db = new TourContext();
        protected List<SelectListItem> list = new List<SelectListItem>();
        public ActionResult Search()
        {
            var tour = db.Tour.ToArray();
            for (int i = 0; i < tour.Length; i++)
            {
                var tour1 = tour.Last();
                if (tour[i].TypeOfTour != tour1.TypeOfTour)
                {
                    string k;
                    if (tour[i].TypeOfTour != tour[i + 1].TypeOfTour)
                    {
                        k = tour[i].TypeOfTour;
                        list.Add(new SelectListItem { Text = k });
                    }
                }
                else
                {
                  string  k = tour[i].TypeOfTour;
                    list.Add(new SelectListItem { Text = k });
                }
            }
                ViewBag.TypeOfTour = list;
                return View(db.Tour.ToList());
        }
       
        [HttpPost]
        public ActionResult Search(string Date= null,string TypeOfTour = null)
        {
            var tour = db.Tour.ToArray();
            for (int i = 0; i < tour.Length; i++)
            {
                var tour1 = tour.Last();
                if (tour[i].TypeOfTour != tour1.TypeOfTour)
                {
                    string k;
                    if (tour[i].TypeOfTour != tour[i + 1].TypeOfTour)
                    {
                        k = tour[i].TypeOfTour;
                        list.Add(new SelectListItem { Text = k });
                    }
                }
                else
                {
                    string k = tour[i].TypeOfTour;
                    list.Add(new SelectListItem { Text = k });
                }
            }
            ViewBag.TypeOfTour = list;
          
            
            if (Date !="" && TypeOfTour =="")
            {
                int Date1 = Int32.Parse(Date);
              var  data = db.Tour.Where(m=>m.DateTour.Any(k=>k.FirstDate.Month == Date1));
              return View(data);
            }
            if(TypeOfTour != "" && Date =="")
            {
                var data = db.Tour.Where(m => m.TypeOfTour == TypeOfTour);
                return View(data);
            }
            if(TypeOfTour !="" && Date !="")
            {
                int Date1 = Int32.Parse(Date);
                var data = db.Tour.Where(m => m.DateTour.Any(k => k.FirstDate.Month == Date1)).Where(m=>m.TypeOfTour == TypeOfTour);
                return View(data);
            }
            return View(db.Tour.ToList());
        }
    }
}
