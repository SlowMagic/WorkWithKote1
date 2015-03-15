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
                new Tour{NameTour ="Paris", TitleTour="Paris is the capital of Francе",DescriptionTour ="Главный политический, экономический и культурный центр Франции. Относится к глобальным городам, мировым финансовым центрам. Штаб-квартира ЮНЕСКО и других международных организаций"+
"Исторический центр, образованный островом Сите и обоими берегами Сены, складывался на протяжении веков. Во второй половине XIX века претерпел коренную реконструкцию. В пригороде расположен дворцово-парковый ансамбль Версаль",Cost = 1500,Valuta = "Доллар",PrePay = 500,Reservation="Есть",IsBus = true,TourStatus ="Неактивный",TypeOfTour="Гастрономический",PodpicePrice = "Аукцонная",AukcionPrice=1200,SuppName="Илья Давиденко",SuppFoto="/UpLoad/SuppFoto/1eae1900-0241-4bec-bdf0-ac3bb00cbc10.jpg",SuppDiscription="Веселый, интересный, не умеет играть в доту",SuppVkontakte="http://vk.com/david_enko",KindOfPay="Наличный",People=15,AllPeople=50,TourImg="/UpLoad/TourImg/ca196c77-a905-4a9b-abe2-f1459784f342.png"},
               new Tour{NameTour ="Paris", TitleTour="Paris is the capital of Francе",DescriptionTour ="Главный политический, экономический и культурный центр Франции. Относится к глобальным городам, мировым финансовым центрам. Штаб-квартира ЮНЕСКО и других международных организаций"+
"Исторический центр, образованный островом Сите и обоими берегами Сены, складывался на протяжении веков. Во второй половине XIX века претерпел коренную реконструкцию. В пригороде расположен дворцово-парковый ансамбль Версаль",Cost = 1500,Valuta = "Доллар",PrePay = 500,Reservation="Есть",IsBus = true,TourStatus ="Неактивный",TypeOfTour="Гастрономический",PodpicePrice = "Аукцонная",AukcionPrice=1200,SuppName="Илья Давиденко",SuppFoto="/UpLoad/SuppFoto/1eae1900-0241-4bec-bdf0-ac3bb00cbc10.jpg",SuppDiscription="Веселый, интересный, не умеет играть в доту",SuppVkontakte="http://vk.com/david_enko",KindOfPay="Наличный",People=15,AllPeople=50,TourImg="/UpLoad/TourImg/ca196c77-a905-4a9b-abe2-f1459784f342.png"},
                new Tour{NameTour ="Paris", TitleTour="Paris is the capital of Francе",DescriptionTour ="Главный политический, экономический и культурный центр Франции. Относится к глобальным городам, мировым финансовым центрам. Штаб-квартира ЮНЕСКО и других международных организаций"+
"Исторический центр, образованный островом Сите и обоими берегами Сены, складывался на протяжении веков. Во второй половине XIX века претерпел коренную реконструкцию. В пригороде расположен дворцово-парковый ансамбль Версаль",Cost = 1500,Valuta = "Доллар",PrePay = 500,Reservation="Есть",IsBus = true,TourStatus ="Неактивный",TypeOfTour="Шопинг",PodpicePrice = "Аукцонная",AukcionPrice=1200,SuppName="Илья Давиденко",SuppFoto="/UpLoad/SuppFoto/1eae1900-0241-4bec-bdf0-ac3bb00cbc10.jpg",SuppDiscription="Веселый, интересный, не умеет играть в доту",SuppVkontakte="http://vk.com/david_enko",KindOfPay="Наличный",People=15,AllPeople=50,TourImg="/UpLoad/TourImg/ca196c77-a905-4a9b-abe2-f1459784f342.png"},
                 new Tour{NameTour ="Paris", TitleTour="Paris is the capital of Francе",DescriptionTour ="Главный политический, экономический и культурный центр Франции. Относится к глобальным городам, мировым финансовым центрам. Штаб-квартира ЮНЕСКО и других международных организаций"+
"Исторический центр, образованный островом Сите и обоими берегами Сены, складывался на протяжении веков. Во второй половине XIX века претерпел коренную реконструкцию. В пригороде расположен дворцово-парковый ансамбль Версаль",Cost = 1500,Valuta = "Доллар",PrePay = 500,Reservation="Есть",IsBus = true,TourStatus ="Неактивный",TypeOfTour="Развлекательный",PodpicePrice = "Аукцонная",AukcionPrice=1200,SuppName="Илья Давиденко",SuppFoto="/UpLoad/SuppFoto/1eae1900-0241-4bec-bdf0-ac3bb00cbc10.jpg",SuppDiscription="Веселый, интересный, не умеет играть в доту",SuppVkontakte="http://vk.com/david_enko",KindOfPay="Наличный",People=15,AllPeople=50,TourImg="/UpLoad/TourImg/ca196c77-a905-4a9b-abe2-f1459784f342.png"},
            };
            tour.ForEach(c => context.Tour.Add(c));
            context.SaveChanges();
            var date = new List<DateTour>
            {
                new DateTour {TourId = 1,FirstDate = DateTime.Parse("2015-09-01"),SecondDate= DateTime.Parse("2014-11-20")},
                new DateTour {TourId = 1, FirstDate = DateTime.Parse("2015-10-02"),SecondDate= DateTime.Parse("2016-11-02")},
                new DateTour {TourId = 2, FirstDate=DateTime.Parse("2016-11-02"),SecondDate= DateTime.Parse("2018-12-03")},
                new DateTour {TourId = 3, FirstDate= DateTime.Parse("2015-03-12"),SecondDate = DateTime.Now},
                new DateTour {TourId = 4, FirstDate= DateTime.Parse("2015-01-12"),SecondDate = DateTime.Now}
            };
            date.ForEach(c => context.DateTours.Add(c));
            context.SaveChanges();
            var uslugi = new List<DopUslug>
           {
               new DopUslug{TourId=1, Name="Автобус",Price=20},
               new DopUslug{TourId=1,Name="Двухразовое питание",Price=50},
               new DopUslug{TourId=2,Name="Двухразовое питание",Price=35},
               new DopUslug{TourId=3,Name="Двухразовое питание",Price=25},
                new DopUslug{TourId=4,Name="Двухразовое питание",Price=125}
           };
            uslugi.ForEach(c => context.DopUslugs.Add(c));
            context.SaveChanges();
        }
    }
}
