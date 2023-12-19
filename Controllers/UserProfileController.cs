using Poll4u_Web_BAL;
using Poll4u_Web_BOL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Poll4u_Web.Controllers
{
    public class UserProfileController : Controller
    {
        // GET: UserProfile
        
        public ActionResult Profile(int id)
        {
            User_BAL p_data = new User_BAL();
            Admin_User_BAL u_data = new Admin_User_BAL();
            dynamic model = new ExpandoObject();
            model.User= u_data.Details_User(id);
            model.Poll = p_data.Get_PollUser(id);
            return View(model);
        }
        [Authorize]
        public ActionResult User_MyProfile(int id=0,string Name="")
        {
            User_BAL p_data = new User_BAL();
            Admin_User_BAL u_data = new Admin_User_BAL();
            dynamic model = new ExpandoObject();
            model.User = u_data.Details_User(id);
            
            if(Name == "Liked")
            {
                ViewBag.lactive = "active";
               
                ViewBag.Action = "Like";
                model.Poll = p_data.UserLiked_Poll(id);
            }
            else if(Name=="Expire")
            {
                ViewBag.eactive = "active";
                
                ViewBag.Action = "Expire";
                model.Poll = p_data.Get_ExpirePollUser(id);
            }
            else
            {
                ViewBag.PollMessage = TempData["poll_Message"];
                ViewBag.dactive = "active";
                ViewBag.Action = "Dashboard";
                model.Poll = p_data.Get_PollUser(id);
            }
            return View(model);
        }
        [Authorize]
        public ActionResult Edit_Profile(int id)
        {
            Admin_User_BAL u_data = new Admin_User_BAL();
            var details = u_data.Details_User(id);
            return View(details);
        }
        [HttpPost]
        public ActionResult Edit_Profile(User u)
        {
            int data;
            if (u.Profile_Action == "New")
            {
                string path = Server.MapPath("~/Content/Profile Photo/");
                string extension = Path.GetExtension(u.save_Profile.FileName);
                string fileName = u.Username + extension;
                string fullPath = Path.Combine(path, fileName);
                u.save_Profile.SaveAs(fullPath);

                User_BAL edit = new User_BAL();
                u.profile = fileName;
                 data = edit.Edit_User(u);
            }
            else if(u.Profile_Action== "Remove")
            {
                u.profile = "Blank Profile.png";
                User_BAL edit = new User_BAL();
                data = edit.Edit_User(u);
            }
            else
            {
                User_BAL edit = new User_BAL();
                 data = edit.Edit_User(u);
            }
            if (data == 1)
            {
                return RedirectToAction("User_MyProfile", "UserProfile", new { @id = u.user_id });
            }
            else
            {
                ViewBag.Message = "Please Try Again";
                return View();
            }
        }
        
        public JsonResult Follow(int u_id)
        {
            User_BAL data = new User_BAL();
            int u_id_f = (int)Session["user_id"];
            int details = data.User_follow(u_id, u_id_f);
            return Json(details,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Unfollow(int u_id)
        {
            User_BAL data = new User_BAL();
            int u_id_f = (int)Session["user_id"];
            int details = data.User_Unfollow(u_id, u_id_f);
            return Json(details, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Get_UserFollow(int u_id,int u_id_f)
        {
            User_BAL getf = new User_BAL();
            int details = getf.Get_UserFollow(u_id, u_id_f);
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Edit_Username(string Username)
        {
            int user_id = (int)Session["user_id"];
            User_BAL data = new User_BAL();
            int details = data.edit_username(Username, user_id);
            return Json(details,JsonRequestBehavior.AllowGet);
        }
    }
}