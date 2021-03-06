﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebMatrix.WebData;
using WorkWithKOTE.Models;
using System.Data;
namespace WorkWithKOTE.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        UsersContext db = new UsersContext();
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult SearchAllUsers()
        {
            return View(db.UserProfiles.ToList());
        }

        [HttpPost]
        public ActionResult SearchAllUsers(string mail = null, string name = null, string surname = null, string age = null, string sex = null)
        {
            IQueryable<UserProfile> data = db.UserProfiles;
            if (!String.IsNullOrEmpty(mail))
                data = data.Where(m => m.Email == mail);
            if (!String.IsNullOrEmpty(name))
                data = data.Where(m => m.RuFirstName == name);
            if (!String.IsNullOrEmpty(surname))
                data = data.Where(m => m.RuSecondName == surname);
            if (!String.IsNullOrEmpty(age))
            {
                int bithdayYear = DateTime.Now.Year - int.Parse(age);
                data = data.Where(m => m.Birthday.Value.Year == bithdayYear);
            }
            if (!String.IsNullOrEmpty(sex))
                data = data.Where(m => m.Sex == sex);
            return View(data);
        }
        [Authorize(Roles = "Admin")]
        public ActionResult RolesForUser(string moder, int id)
        {
            var roles = (SimpleRoleProvider)Roles.Provider;
            var membership = (SimpleMembershipProvider)Membership.Provider;
            if (!roles.RoleExists("Moderator"))
                roles.CreateRole("Moderator");
            if (moder != null)
            {
                if (membership.GetUser(moder, false) == null)
                {
                    ViewBag.Message = "Такой почтовый ящик не зарегестрирован";
                    return View();
                }
            }
            if (!roles.IsUserInRole(moder, "Moderator"))
                roles.AddUsersToRoles(new[] { moder }, new[] { "Moderator" });
            return RedirectToAction("Profile", "Profile", new { id = id });
        }
        [HttpPost]
        public ActionResult BonusForUser(int UserId, int? UserBonus)
        {
            if (UserBonus == null)
            {
                UserBonus = 0;
            }
            var user = db.UserProfiles.Find(UserId);
            if (user.Bonus != null)
                user.Bonus = user.Bonus.Value + UserBonus;
            else
                user.Bonus = UserBonus;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Profile", "Profile", new { id = UserId });
        }
        [Authorize(Roles = "Admin")]
        public ActionResult SubmitTrip(int? id)
        {
            if (id != null)
            {
                var trip = db.Trip.Find(id);
                trip.Status = "Оплачена";
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile", "Profile", new { id = trip.UserId });
            }
            return RedirectToAction("Error", "Error");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult CancleTrip(int? id)
        {
            if (id != null)
            {
                var trip = db.Trip.Find(id);
                trip.Status = "Отклонена";
                var tour = db.Tour.Find(trip.TourId);
                tour.People = tour.People.Value - 1;
                db.Entry(trip).State = EntityState.Modified;
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile", "Profile", new { id = trip.UserId });
            }
            return RedirectToAction("Error", "Error");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteTrip(int? id)
        {
            if (id != null)
            {
                var trip = db.Trip.Find(id);
                var tour = db.Tour.Find(trip.TourId);
                tour.People = tour.People.Value - 1;
                db.Entry(trip).State = EntityState.Deleted;
                db.Entry(tour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Profile", "Profile", new { id = trip.UserId });
            }
            return RedirectToAction("Error", "Error");
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Cursed()
        {
            return PartialView(db.Curseds.Single());
        }
        [HttpPost]
        public ActionResult Cursed(Cursed cursed)
        {
            db.Entry(cursed).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Profile", "Profile");
        }
    }
}
