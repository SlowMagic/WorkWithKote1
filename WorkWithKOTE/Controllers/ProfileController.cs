using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using WorkWithKOTE.Filters;
using WorkWithKOTE.Models;
using System.Data;
using System.IO;
using System.Net.Mail;
using System.Data.Entity;
using WorkWithKOTE.Code;
namespace WorkWithKOTE.Controllers
{
    [InitializeSimpleMembership]
    public class ProfileController : Controller
    {
        //
        UsersContext db = new UsersContext();
        // GET: /Account/Logi

        [Authorize]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Profile(int? id)
        {
            if (id != null)
            {
                if (User.IsInRole("Admin"))
                {
                    UserProfile user = db.UserProfiles.Include(m => m.VisitedTours).First(m => m.UserId == id);
                    ViewBag.UserRole = Roles.GetRolesForUser(user.Email);
                    ViewBag.Id = new SelectList(db.Trip.Where(m => m.UserId == id), "TripID", "TripID");
                    return View(user);
                }
                else
                {
                    int i = WebSecurity.GetUserId(User.Identity.Name);
                    UserProfile user = db.UserProfiles.Include(m => m.VisitedTours).First(m => m.UserId == i);
                    ViewBag.UserRole = Roles.GetRolesForUser(User.Identity.Name);
                    ViewBag.Id = new SelectList(db.Trip.Where(m => m.UserId == i), "TripID", "TripID");
                    return View(user);
                }
            }
            else
            {
                    int i = WebSecurity.GetUserId(User.Identity.Name);
                    UserProfile user = db.UserProfiles.Include(m => m.VisitedTours).First(m => m.UserId == i);
                    ViewBag.UserRole = Roles.GetRolesForUser(User.Identity.Name);
                    ViewBag.Id = new SelectList(db.Trip.Where(m => m.UserId == i && m.Status == "Не оплачена"), "TripID", "TripID");  
                    return View(user);
            }
           
        }
        [Authorize]
        public ActionResult EditProfile()
        {

            int i = WebSecurity.GetUserId(User.Identity.Name);
            UserProfile user = db.UserProfiles.Find(i);
            return View(user);
        }
        public ActionResult UserProfileBonus(int UserId)
        {
            var user = db.UserProfiles.Find(UserId);
            return PartialView(user);
        }
        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(UserProfile model, HttpPostedFileBase file)
        {
            int i = WebSecurity.GetUserId(User.Identity.Name);
            var check = UploadImages.UploadImg(file, "/UpLoad/Avatar/"); 
            if (check != null)
                model.Avatar = check;
            model.UserId = i;
            model.Email = User.Identity.Name;
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Profile", "Profile");
        }
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost, AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(string Email)
        {
            var user = Membership.GetUser(Email);
            int i = WebSecurity.GetUserId(Email);
            if (user == null)
            {
                TempData["Message"] = "Данный E-mail не зарегестрирован";
            }
            else
            {
                var token = WebSecurity.GeneratePasswordResetToken(Email);
                var resetLink = "<a href=\"" + Url.Action("ResetPassword", "Profile", new { un = i, rt = token }, "http") + "\">ReserPasswor</a>";
                string subject = "Если вы хотите сбросить пароль то перейдите по данной ссылке";
                string body = "<b>Если вы хотите сбросить пароль то перейдите по данной ссылке</b><br/>" + resetLink +" Операция необратимая. Пароль будет выслан на ваш ящик через 1 секунду";
                try
                {
                    SendMail(Email, subject, body);
                    TempData["Message"] = "На ваш почтовый ящик было отправленно письмо";
                }

                catch (Exception ex)
                {
                    TempData["Message"] = "Возникла ошибка :" + ex;
                }
            }
            return View();
        }
        public ActionResult ResetPassword(int un, string rt)
        {
            var userProfile = db.UserProfiles.
                FirstOrDefault(x => x.UserId.Equals(un));
            if (userProfile == null)
            {
                return RedirectToAction("BadLink");
            }
            var membership = WebSecurity.GetUserIdFromPasswordResetToken(rt);
            if (userProfile.UserId != membership)
            {
                return RedirectToAction("BadLink");
            }
            string newpassword = GenerateRandomPassword(6);
            if (!WebSecurity.ResetPassword(rt, newpassword))
            {
                return RedirectToAction("BadLink");
            }

            // высылаем письмо с новым паролем
            string subject = "Новый пароль";
            string body = "<b>Новый пароль. Вводить учитывая регистр и все символы и ручками. Не копировать.</b><br>" + newpassword;
            try
            {
                SendMail(userProfile.Email, subject, body);
                ViewBag.Message = "Письмо с паролем выслано.";
            }
            catch (Exception ex)
            {
                ViewBag.Message = "Возникла ошибка при отсылки письма. " +
                                                          ex.Message;
            }
            return RedirectToAction("Index", "Home");
        }
        private string GenerateRandomPassword(int length)
        {
            string allowedChars = "abcdefghijkmnopqrstuvwxyz" +
                                         "ABCDEFGHJKLMNOPQRSTUVWXYZ" +
                                         "0123456789!@$?_-*&#+";
            char[] chars = new char[length];
            Random rd = new Random();
            for (int i = 0; i < length; i++)
            {
                chars[i] = allowedChars[rd.Next(0, allowedChars.Length)];
            }
            return new string(chars);
        }
        private void SendMail(string emailid, string subject, string body)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;

            System.Net.NetworkCredential credentials =
              new System.Net.NetworkCredential("LevitskiyOrange@gmail.com", "orange123654789");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;

            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("LevitskiyOrange@gmail.com");
            msg.To.Add(new MailAddress(emailid));

            msg.Subject = subject;
            msg.IsBodyHtml = true;
            msg.Body = body;

            client.Send(msg);
        }
        [ChildActionOnly]
        public ActionResult UsersTour(int id = 0)
        {
            var data = db.Trip.Include(m => m.DateTour).Include(m => m.Tour).Include(m=>m.User).Where(m => m.UserId == id);
            return PartialView(data);
        }
    }
}
