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
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "День Рождения")]
        public DateTime? BirthDay { get; set; }
        public string Citizenship { get; set; }
        public string Pasport { get; set; }
        [Required(ErrorMessage = "Номер телефона должен быть указан")]
        public string mobile { get; set; }
        [Required(ErrorMessage = "Почтовый ящик должен быть указан")]
        [RegularExpression(@"^(|(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-+)|(\w+\.))*\w{1,63}\.[a-zA-Z]{2,6})$", ErrorMessage = "Е-Mail введен не верно")]
        public string email { get; set; }
        public int DateTourId { get; set; }
        public DateTour DateTour { get; set; }
        public string Valuta { get; set; }
        public string Status { get; set; }
        public float? Bonus { get; set; }
        public float? BonusPay { get; set; }
        public int UserId { get; set; }
        public UserProfile User { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
    }
    [Table ("Tour")]
   public class Tour
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
       public int TourId { get; set; }
       [Required(ErrorMessage = "Поле Название  должно быть установлено")]
       public string NameTour {get;set;}
       [Required(ErrorMessage = "Поле Заголовок  быть установлено")]
       public string TitleTour { get; set; }
      [AllowHtml]
       public string DescriptionTour { get; set; }
        [Required(ErrorMessage = "Цена тура не может быть пустой ")]
       [RegularExpression(@"\-?\d+(\.\d{0,})?", ErrorMessage = "Ошибка поле Стоимость .Не верно введено число.Возможно вы поставили , вместо .")]
       public decimal? Cost { get; set; }
       public string Valuta { get; set; }
       [RegularExpression(@"\-?\d+(\.\d{0,})?", ErrorMessage = "Ошибка поле Стоимость .Не верно введено число.Возможно вы поставили , вместо .")]
       public decimal? PrePay { get; set; }
       public string Reservation { get; set; }
       public bool IsBus { get; set; }
       public bool IsAriplane { get; set; }
       public bool IsShip { get; set; }
       public bool IsTrain { get; set; }
       public string PlaceOfDeparture { get; set; }
       public string PlaceOfArrival { get; set; }
       public int  TourStatusId { get; set; }
       public TourStatus TourStatus { get; set; }
       public int   TypeOfTourId { get; set; }
       public TypeOfTour TypeOfTour { get; set; }
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
       public List<SameTour> SameTour { get; set; }
       public string Document { get; set; }
       public float? Bonus { get; set; }
       public IList<Tag> Tag { get; set; }
       public IList<RoutePoint> RoutePoints { get; set; }
       public IList<DateTour> DateTour { get; set; }
       public IList<DopUslug> DopUslug { get; set; }
       public int? GalleryID { get; set; }
       public int LogoId { get; set; }
       public BigLogo BigLogo { get; set; }
       public List<Trip> Trips { get; set; }

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
    }
    [Table ("DateTour")]
    public class DateTour
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DateTourId { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType (DataType.Date)]
        [DisplayFormat(DataFormatString="{0:dd'.'MM'.'yyyy}",ApplyFormatInEditMode=true)]
        [Display(Name="Дата отправки")]
        public DateTime FirstDate { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата приезда")]
        public DateTime SecondDate { get; set; }
        public List<Trip> Trips { get; set; }
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
        public Tour TourHightPrev { get; set; }
        public List<Tour> TourHight { get; set; }
        public Tour TourCenterPrev { get; set; }
        public List<Tour> TourCenter { get; set; }
        public Tour TourDownPrev { get; set; }
        public List<Tour> TourDown { get; set; }
        public List<Tour> TourMain { get; set; }
    }
    [Table ("News")]
    public class News
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int NewsId { get; set; }
        public string NewsTitle { get; set; }
        public string NewsContent { get; set; }
        [AllowHtml]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public string NewsBody { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Поле должно быть установлено")]
        public DateTime NewsDate { get; set; }
    }
    [Table ("TourStatus")]
    public class TourStatus
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TourStatusId { get; set; }
        public string TourStatusName { get; set; }
    }
    public class TypeOfTour
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int TypeOfTourId { get; set; }
        public string TypeOfTourName { get; set; }

    }
 
    [Table("SameTour")]
    public class SameTour
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }
        public int SameTourID { get; set; }
        public string SameTourName { get; set; }
    }
    [Table("BigLogo")]
    public class BigLogo
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int LogoId { get; set; }
        public string LogoName { get; set; }
        public string UrlImg { get; set; }
    }
    [Table ("VisitedTour")]
    public class VisitedTour
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int VisitedTourId { get; set; }
        public int UserId { get; set; }
        public UserProfile User { get; set; }
        public string TourName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FirstDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? SecondDate { get; set; }

    }
    public class ProfileTour
    {
        public List<Tour> tours { get; set; }
        public List<Trip> trips { get; set; }
        public List<DateTour> date { get;set; }
    }
}