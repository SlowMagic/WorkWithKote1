using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using WorkWithKOTE.Models;
namespace WorkWithKOTE.Filters
{
    public static class DefaultValueForTour
    {
        public static  void DefaultValueForTourStatus()
        {
            UsersContext db = new UsersContext();
            var TourStatus = db.TourStatus.ToList();
            if (TourStatus.Count == 0)
            {
               db.TourStatus.Add(new TourStatus { TourStatusName = "Активный" });
               db.TourStatus.Add(new TourStatus { TourStatusName = "Неактивный" });
               db.TourStatus.Add(new TourStatus { TourStatusName = "Рекомендуемый" });
               db.TourStatus.Add(new TourStatus { TourStatusName = "Превью главной" });
               db.TourStatus.Add(new TourStatus { TourStatusName = "Превью блока" });
               db.SaveChanges();
            }
           
        }
        public static void DefaultValueForTourTypeOfTour()
        {
            UsersContext db = new UsersContext();
            var TypeOfTour = db.TypeOfTours.ToList();
            if(TypeOfTour.Count == 0)
            {
                db.TypeOfTours.Add(new TypeOfTour { TypeOfTourName = "Туры по Украине" });
                db.TypeOfTours.Add(new TypeOfTour { TypeOfTourName = "Туры в Грузию" });
                db.TypeOfTours.Add(new TypeOfTour { TypeOfTourName = "Приключенческие туры" });

                db.SaveChanges();
            }
        }
       
    }
}