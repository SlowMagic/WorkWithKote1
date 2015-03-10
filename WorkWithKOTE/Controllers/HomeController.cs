using DotNetOpenAuth.OpenId.Extensions.SimpleRegistration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using WorkWithKOTE.Filters;
using WorkWithKOTE.Models;

namespace WorkWithKOTE.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        TourContext db = new TourContext();
        public ActionResult Index()
        {
            var roles = (SimpleRoleProvider)Roles.Provider;

            //Получаем провайдер членства
            var membership = (SimpleMembershipProvider)Membership.Provider;

            // Если нет в системе роли admin, создаём её
            if (!roles.RoleExists("Admin"))
                roles.CreateRole("Admin");

            // Если нет в системе пользователя admin, создаём его(в этом месте ошибка)
            if (membership.GetUser("LevitskiyOrange@gmail.com", false) == null)
            {
                membership.CreateUserAndAccount("LevitskiyOrange@gmail.com", "123654789");
            }

            // Если у пользователя admin нет роли admin, присваиваем ему эту роль
            if (!roles.IsUserInRole("LevitskiyOrange@gmail.com","Admin"))
                roles.AddUsersToRoles(new[] { "LevitskiyOrange@gmail.com" }, new[] { "Admin" });
            var data = new TourForHomePage();
            string k = "Вверх";
            data.TourHight = db.Tour.Where(m=>m.TypeOfTour == k).ToList();
            data.TourCenter = db.Tour.Where(m => m.TypeOfTour == "Середина").ToList();
            data.TourDown = db.Tour.Where(m => m.TypeOfTour =="Низ").ToList();
            return View(data);
        }
  
        public ActionResult DateForCurrentTour(int id)
        {
            
            var date = db.DateTours.Where(m => m.TourId== id);
          
            return PartialView(date);
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
