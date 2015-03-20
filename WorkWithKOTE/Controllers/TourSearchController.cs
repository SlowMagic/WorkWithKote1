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
       // TourContext db = new TourContext();
        UsersContext db = new UsersContext();
        public List<SelectListItem> Selectlist() {
            List<SelectListItem> list = new List<SelectListItem>();
            var tour = db.Tour.ToArray();
            for (int i = 0; i < tour.Length; i++)//Здесь мы находим все типы тура и повторяющиеся значения мы не добавляем  в коллекцию выпадающего списка
            {
                string k;
                var tour1 = tour.Last();
                if (tour[i].TourId != tour1.TourId)
                {
                    if (tour[i].TypeOfTour != tour[i + 1].TypeOfTour)
                    {
                        k = tour[i].TypeOfTour;
                        list.Add(new SelectListItem { Text = k });
                    }
                }
                else if (tour[i].TypeOfTour != tour[0].TypeOfTour)
                {
                    k = tour1.TypeOfTour;
                    list.Add(new SelectListItem { Text = k });
                }
            }
            ViewBag.TypeOfTour = list;
            return list ;
        }//Здесь мы находим все типы тура и повторяющиеся значения мы не добавляем  в коллекцию выпадающего списка
        public ActionResult Search()
        {
                ViewBag.TypeOfTour = Selectlist();
                return View(db.Tour.ToList());
        }
       
        [HttpPost]
        public ActionResult Search(String Date= null,String TypeOfTour = null)
        {
            ViewBag.TypeOfTour = Selectlist();
            var data = db.Tour.AsQueryable();
            if(!String.IsNullOrEmpty(Date))
            {
                int Date1 = Int32.Parse(Date);
                var  data1 = db.Tour.Where(m => m.DateTour.Any(k => k.FirstDate.Month == Date1));
               data = data1;
            }
            
            if(!String.IsNullOrEmpty(TypeOfTour))
            {
               var  data1 = db.Tour.Where(m => m.TypeOfTour == TypeOfTour);
               data = data1;
            }
           /* if (Date !="" && TypeOfTour =="")
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
            }*/
            return View(data);
        }
    }
}
