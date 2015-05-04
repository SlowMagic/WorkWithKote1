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
       public static void DefaultValueForBigLogo()
        {
            UsersContext db = new UsersContext();
            var BigLogo = db.BigLogos.ToList();
           if(BigLogo.Count == 0)
           {
               db.BigLogos.Add(new BigLogo { LogoName = "Красный логотип", UrlImg = "/Content/img/small-logo-red.png" });
               db.BigLogos.Add(new BigLogo { LogoName = "Белый логотип", UrlImg = "/Content/img/small-logo.png" });
           }
           db.SaveChanges();
       }
      public static void Anonimus()
       {
           UsersContext db = new UsersContext();
           var anonimus = db.UserProfiles.ToList();
          if(anonimus.Count == 0 )
          {
              db.UserProfiles.Add(new UserProfile { Email = "Anonimus@mail.com" });
              db.SaveChanges();
          }
       }
      public static void Curenci()
      {
          UsersContext db = new UsersContext();
          var curenci = db.Curseds.ToList();
          if (curenci.Count == 0)
          {
              db.Curseds.Add(new Cursed { USD = 22.262405M, Evro = 24.326132M });
              db.SaveChanges();
          }
      }
    }
}