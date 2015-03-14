using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace WorkWithKOTE.Models
{
    public class TourContext:DbContext
    {
        public TourContext ()
            :base("DefaultConnection")
    {

    }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<Tour> Tour { get; set; }
        public DbSet<DateTour> DateTours { get; set; }
        public DbSet<Tag> Teg { get; set; }
        public DbSet<DopUslug> DopUslugs { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<RoutePoint> RoutePoint { get; set; }
    }
    [Table("Trip")]
    public class Trip
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TripID { get; set; }
        public decimal? TourPrice { get; set; }
        public int AmtPeople { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string Sex { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "День Рождения")]
        public DateTime? BirthDay { get; set; }
        public string Citizenship { get; set; }
        public string Pasport { get; set; }
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Номер введён неверно.")]
        public string mobile { get; set; }
        public string email { get; set; }
        public int DateTourId { get; set; }
        public int TourId { get; set; }
        public string Valuta { get; set; }
        public List<UserProfile> Users { get; set; }
    }
    [Table ("Tour")]
   public class Tour
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
       public int TourId { get; set; }
       public string NameTour {get;set;}
       public string TitleTour { get; set; }
       [AllowHtml]
       public string DescriptionTour { get; set; }
       public decimal? Cost { get; set; }
       public string Valuta { get; set; }
       public decimal? PrePay { get; set; }
       public string Reservation { get; set; }
       public bool IsBus { get; set; }
       public bool IsAriplane { get; set; }
       public bool IsShip { get; set; }
       public bool IsTrain { get; set; }
       public string PlaceOfDeparture { get; set; }
       public string PlaceOfArrival { get; set; }
       public string TourStatus { get; set; }
       public string TypeOfTour { get; set; }
       public string PodpicePrice { get; set; }
       public decimal? AukcionPrice { get; set; }
       public string KommisiaAgent { get; set; } 
       public string SuppName { get; set; }
       public string SuppVkontakte { get; set; }
       public string SuppFoto { get; set; }
       public string SuppDiscription { get; set; }
       public string KindOfPay { get; set; }
       public int? People { get; set; }
       public int? AllPeople { get; set; }
       public string TourImg { get; set; }
       public string PohozTour { get; set; }
       public string Document { get; set; }
       public float? Bonus { get; set; }
       public IList<Tag> Tag { get; set; }
       public IList<RoutePoint> RoutePoints { get; set; }
       public IList<DateTour> DateTour { get; set; }
       public IList<DopUslug> DopUslug { get; set; }
       public int? GalleryID { get; set; }

    }
    public class Tag
    {
        public int TagId { get; set; }
        public int TourId { get; set; }
        public string TagName { get; set; }
    }
    public class RoutePoint
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int RoutePointID { get; set; }
        public int TourId { get; set; }
        public double Lat { get; set; }
        public double Lng { get; set; }
        public int Order { get; set; }
    }
    [Table ("DateTour")]
    public class DateTour
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DateTourId { get; set; }
        public int TourId { get; set; }
        [DataType (DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd'/'MM'/'yyyy}",ApplyFormatInEditMode=true)]
        [Display(Name="Дата отправки")]
        public DateTime FirstDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата приезда")]
        public DateTime SecondDate { get; set; }
    }
    [Table ("Gallery")]
    public class Gallery
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int GalleryId { get; set; }
        public string GalleryName { get; set; }
        public string FirstImg { get; set; }
        public string SecondImg { get; set; }
        public string ThirdImg { get; set; }
    }
    [Table ("DopUslug")]
    public class DopUslug
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DopUslugId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int TourId { get; set; }
    }
    public class TourForHomePage
    {
    public   List <Tour> TourHight{get;set;}
    public   List  <Tour> TourCenter{get;set;}
    public   List< Tour >TourDown{get;set;}
    }
    public class DateForMonth
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
 
}