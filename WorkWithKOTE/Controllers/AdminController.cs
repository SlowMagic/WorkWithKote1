using System;
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
        [Authorize(Roles = "Admin")]
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
        public ActionResult RolesForUser(string moder)
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
            return RedirectToAction("SearchAllUsers", "Admin");
        }
        [HttpPost]
        public ActionResult BonusForUser(int UserId, int? UserBonus)
        {
            if (UserBonus == null){
                UserBonus = 0;
            }
            var user = db.UserProfiles.Find(UserId);
            if (user.Bonus != null)
                user.Bonus = user.Bonus.Value + UserBonus;
            else
                user.Bonus = UserBonus;
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Profile","Profile", new { id = UserId });
        }
    }
}
