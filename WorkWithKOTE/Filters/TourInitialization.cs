using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WorkWithKOTE.Models;
using System.Data.Entity.Infrastructure;
using WebMatrix.WebData;
using System.Web.Mvc;
using System.Threading;
namespace WorkWithKOTE.Filters
{
    public class TourInitialization:DropCreateDatabaseIfModelChanges<TourContext>
    {
        protected override void Seed(TourContext context)
        {
            var tour = new List<Tour>
            {
                new Tour{NameTour ="Paris", TitleTour="Paris is the capital of Francе",DescriptionTour ="jdnfskndkfsnfs",Cost = 1500,Valuta = "Доллар",PrePay = 500,
                Reservation="Есть",IsBus = true,TourStatus ="Неактивный",TypeOfTour="Середина",PodpicePrice = "Аукцонная"},
                 new Tour{NameTour ="London", TitleTour="London is the capital of Great Britain",DescriptionTour ="jdnfskndkfsnfs",Cost = 1500,Valuta = "Доллар",PrePay = 500,
                Reservation="Есть",IsBus = true,TourStatus ="Неактивный",TypeOfTour="Вверх",PodpicePrice = "Аукцонная"},
                 new Tour{NameTour ="Kiev", TitleTour="Kiev is the capital of Ukraine",DescriptionTour ="jdnfskndkfsnfs",Cost = 1500,Valuta = "Доллар",PrePay = 500,
                Reservation="Есть",IsBus = true,TourStatus ="Неактивный",TypeOfTour="Низ",PodpicePrice = "Аукцонная"}
            };
            tour.ForEach(c => context.Tour.Add(c));
            context.SaveChanges();
            var date = new List<DateTour>
            {
                new DateTour {TourId = 1,FirstDate = DateTime.Parse("2015-09-01"),SecondDate= DateTime.Parse("2014-11-20")},
                new DateTour {TourId = 1, FirstDate = DateTime.Parse("2015-10-02"),SecondDate= DateTime.Parse("2016-11-02")},
                new DateTour {TourId = 2, FirstDate=DateTime.Parse("2016-11-02"),SecondDate= DateTime.Parse("2018-12-03")},
                new DateTour {TourId = 3, FirstDate= DateTime.Parse("2015-03-12"),SecondDate = DateTime.Now}
            };
            date.ForEach(c => context.DateTours.Add(c));
            context.SaveChanges();
            var uslugi = new List<DopUslug>
           {
               new DopUslug{TourId=1, Name="Автобус",Price=20},
               new DopUslug{TourId=1,Name="Двухразовое питание",Price=50},
               new DopUslug{TourId=2,Name="Двухразовое питание",Price=35},
               new DopUslug{TourId=3,Name="Двухразовое питание",Price=25}
           };
            uslugi.ForEach(c => context.DopUslugs.Add(c));
            context.SaveChanges();
        }
    }
}
