using Poll4u_Web_BAL;
using Poll4u_Web_BOL;
using Poll4u_Web_DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Poll4u_Web.Controllers
{

    public class LoginController : Controller
    {

        public ActionResult HomePage()
        {
            return View();
        }
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Admin a,User u)
        {
            Admin_User_BAL a_login = new Admin_User_BAL();
            int r = a_login.Admin_Login(a);
            if(r==1)
            {
              
                FormsAuthentication.SetAuthCookie(a.username, false);
                return RedirectToAction("Report", "AdminDashboard");
            }
            else if(r==2)
            {
                ViewBag.Errormessage = "Smothing Wrong Please Re-Load the Page";
                return View();
            }
            else
            {
                User_BAL u_login = new User_BAL();
                int i = u_login.User_Login(u);
                if (i == 1)
                {
                    FormsAuthentication.SetAuthCookie(u.Username, false);
                    User_BAL data = new User_BAL();
                    var details = data.GetDataFrom_Username(u.Username);
                    Session["user_id"] = details[0].user_id;
                    Session["FirstName"] = details[0].FirstName;
                    Session["LastName"] = details[0].LastName;
                    Session["email"] = details[0].E_Mail;
                    Session["Profile"] = details[0].profile;
                    return RedirectToAction("HomePage", "User_Dashboard");
                }
                else
                {
                    ViewBag.Errormessage = "Invalid Username And Password";
                    return View();
                }
            }
        }

        public JsonResult Checkusername(string Username)
        {
            User_BAL check = new User_BAL();
            int i = check.Check_username(Username);
            if(i==1)
            {
                return Json(true); 
            }
            else
            {
                return Json(false);
            }
            
        }
        public JsonResult GetData(string Username)
        {
            User_BAL data = new User_BAL();
            var details=data.GetDataFrom_Username(Username);
            
            return Json(details,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Search_User(string Username)
        {
            User_BAL data = new User_BAL();
            var details = data.Search_User(Username);
            return Json(details,JsonRequestBehavior.AllowGet);
        }
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User_Signup us)
        {
            User_BAL Check = new User_BAL();
            int i = Check.Check_username(us.Username);
            if (i == 1)
            {
                ViewBag.Message = "Please Follow the Instructions";
                return View();
            }
            else
            {
                User_BAL Signup = new User_BAL();
                int j = Signup.User_Signup(us);
                if (j == 1)
                {
                    FormsAuthentication.SetAuthCookie(us.Username, false);
                    User_BAL data = new User_BAL();
                    var details = data.GetDataFrom_Username(us.Username);
                    Session["user_id"] = details[0].user_id;
                    Session["FirstName"] = details[0].FirstName;
                    Session["LastName"] = details[0].LastName;
                    Session["email"] = details[0].E_Mail;
                    Session["Profile"] = details[0].profile;
                    return RedirectToAction("Homepage", "User_Dashboard");
                }
                else
                {
                    ViewBag.Message = "Invalid Details";
                    return View();
                }
            }
        }

        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(User u)
        {
            User_BAL data = new User_BAL();
            int i = data.Chg_pass(u);
            if (i == 1)
            {
                User_BAL data2 = new User_BAL();
                data2.rm_otp(u);
                return RedirectToAction("Login","Login");
            }
            else
            {
                return View();
            }
           
        }

        public JsonResult Send_Mail(string E_Mail)
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            var num = _rdm.Next(_min, _max);
            User_BAL data = new User_BAL();
            int i = data.In_otp(E_Mail, num);
            if (i == 1)
            {
                var senderEmail = "vatsalparmar.nichetech@gmail.com";
                var reciverEmail = E_Mail;
                var sub = "Forget Password";
                var body = "<p>Your Otp<p> <br /><hr /><center><h1>" +num + "</h1></center>";
                var password = "rigpkieyyjrkwuzn";
                using (MailMessage mm = new MailMessage(senderEmail, reciverEmail))
                {
                    mm.Subject = sub;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential cred = new NetworkCredential(senderEmail, password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = cred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        
                    }
                    int detail = 1;
                    return Json(detail,JsonRequestBehavior.AllowGet);
                }
               
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult Check_otp(string E_mail,int OTP)
        {
            User_BAL data = new User_BAL();
            int i = data.Ch_otp(E_mail, OTP);
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Check_Useremail(string email)
        {
            User_BAL data = new User_BAL();
            int i = data.Check_useremail(email);
            return Json(i, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            Session.Clear();
            return RedirectToAction("Homepage", "User_Dashboard");
        }
    }
}