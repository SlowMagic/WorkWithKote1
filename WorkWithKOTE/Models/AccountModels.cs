using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace WorkWithKOTE.Models
{
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("DefaultConnection")
        {
        }
        public DbSet<BigLogo> BigLogos { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Trip> Trip { get; set; }
        public DbSet<Tour> Tour { get; set; }
        public DbSet<DateTour> DateTours { get; set; }
        public DbSet<Tag> Teg { get; set; }
        public DbSet<DopUslug> DopUslugs { get; set; }
        public DbSet<Gallery> Gallery { get; set; }
        public DbSet<RoutePoint> RoutePoint { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<TourStatus> TourStatus { get; set; }
        public DbSet<TypeOfTour> TypeOfTours { get; set; }
    }

    [Table("UserProfiles")]
    public class UserProfile
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        public string Email { get; set; }
        public string RuFirstName { get; set; }
        public string RuSecondName { get; set; }
        public string RuThirdName { get; set; }
        public string EngFirstName { get; set; }
        public string EngSecondName { get; set; }
        public string EngThirdName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "День Рождения")]
        public DateTime? Birthday { get; set; }
        public string Nationality { get; set; }
        public string PasportData { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата выдачи паспорта")]
        public DateTime? DatePasport { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'.'MM'.'yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Дата выдачи загран паспорта")]
        public DateTime? DateZagran { get; set; }
        public string Sex { get; set; }
        public string Mobile { get; set; }
        public float? Bonus { get; set; }
        public string Avatar { get; set; }
        public List<Trip> Trips { get; set; } 
    }

    public class RegisterExternalLoginModel
    {
        [Required]
        [Display(Name = "Email")]
        [RegularExpression(@"^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$", ErrorMessage = "Номер введён неверно.")]
        public string Email { get; set; }

        public string ExternalLoginData { get; set; }
    }

    public class LocalPasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class ExternalLogin
    {
        public string Provider { get; set; }
        public string ProviderDisplayName { get; set; }
        public string ProviderUserId { get; set; }
    }
}
