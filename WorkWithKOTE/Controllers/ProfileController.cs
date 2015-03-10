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
namespace WorkWithKOTE.Controllers
{
    [InitializeSimpleMembership]
    public class ProfileController : Controller
    {
        //
        UsersContext db = new UsersContext();
        // GET: /Account/Logi

        [Authorize]
        public ActionResult Upload()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpLoad(HttpPostedFileBase file)
        {

            string filename = Guid.NewGuid().ToString();
            string extension = Path.GetExtension(file.FileName);
            filename += extension;
            int i = WebSecurity.GetUserId(User.Identity.Name);
            UserProfile user = db.UserProfiles.Find(i);
            List<string> extensions = new List<string>() { ".png", ".jpg", ".gif" };
            if (extensions.Contains(extension))
            {
                file.SaveAs(Server.MapPath("/UpLoad/" + filename));
                user.Avatar = "/UpLoad/" + filename;
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            else
            {
                ViewBag.Message = "Ошибка формата загружаемой картинки";
            }
            return RedirectToAction("Profile", "Profile");
        }

        [Authorize]
        public ActionResult Profile()
        {

            int i = WebSecurity.GetUserId(User.Identity.Name);
            UserProfile user = db.UserProfiles.Find(i);
            return View(user);
        }
        [Authorize]
        public ActionResult EditProfile()
        {

            int i = WebSecurity.GetUserId(User.Identity.Name);
            UserProfile user = db.UserProfiles.Find(i);

            return View(user);

        }
        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult EditProfile(UserProfile model)
        {
            int i = WebSecurity.GetUserId(User.Identity.Name);
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
                string body = "<b>Если вы хотите сбросить пароль то перейдите по данной ссылке</b><br/>" + resetLink;
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
            string body = "<b>Новый пароль</b><br>" + newpassword;
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

            return RedirectToAction("Login", "Account");
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

    }
}
