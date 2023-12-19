using Poll4u_Web_BAL;
using Poll4u_Web_BOL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Poll4u_Web.Controllers
{
    
    public class User_DashboardController : Controller
    {
        // GET: User_Dashboard
        
        public ActionResult Homepage(int id=0,string Name="")
        { 
            ViewBag.user_id = Session["user_id"];
            
            if (Name == "Category") 
            {
                if (id == 4)
                {
                    TempData["cat"] = "System";
                     Admin_Poll_BAL poll = new Poll4u_Web_BAL.Admin_Poll_BAL();
                    var data = poll.Get_AdminPolls();
                    return View(data);
                }
                else
                {
                    Admin_Poll_BAL Poll = new Admin_Poll_BAL();
                    var data = Poll.Select_CategoryPoll(id);
                    return View(data);
                }
            }
            else
            {
                Admin_Poll_BAL allpoll = new Admin_Poll_BAL();
                var data = allpoll.Get_AllPolls();
                return View(data);
            }
        }

        public JsonResult Get_Category()
        {
            Admin_Poll_BAL data = new Admin_Poll_BAL();
            var details = data.GetCategory();
            return Json(details,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_Comment(int id)
        {
            User_BAL data = new User_BAL();
            var details = data.Get_UserComment(id);
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_AdminComment(int id)
        {
            Admin_Poll_BAL data = new Admin_Poll_BAL();
            var details = data.Get_AdminPollUComments(id);
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Send_Comment(int poll_id,string cmt)
        {
            int user_id = (int)Session["user_id"];
            User_BAL data = new User_BAL();
            var i = data.User_AddComment(user_id, poll_id, cmt);
            return Json(i,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Send_AdminComment(int poll_id, string cmt)
        {
            int user_id = (int)Session["user_id"];
            Admin_Poll_BAL data = new Admin_Poll_BAL();
            var i = data.User_AdminAddComment(user_id, poll_id, cmt);
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Del_Comment(int id)
        {
            User_BAL data = new User_BAL();
            var i = data.Del_Comment(id);
            return Json(i,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Del_AdminComment(int id)
        {
            Admin_Poll_BAL data = new Admin_Poll_BAL();
            var i = data.Del_AdminComment(id);
            return Json(i, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Get_UserLike(int id) 
        {
            User_BAL data = new User_BAL();
            var details = data.Get_LikeUser(id);
            return Json(details,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add_Like(int poll_id)
        {
            int user_id = (int)Session["user_id"];
            User_BAL data = new User_BAL();
            int details = data.Add_LikeUser(user_id,poll_id);
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Add_AdminLike(int Adminpoll_id)
        {
            int user_id = (int)Session["user_id"];
            Admin_Poll_BAL data = new Admin_Poll_BAL();
            int details = data.Add_AdminLikeUser(user_id, Adminpoll_id);
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RM_Like(int like_id)
        {
            User_BAL data = new User_BAL();
            int details = data.RM_LikeUser(like_id);
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        public JsonResult update_Like(int poll_id)
        {
            ViewBag.user_id = Session["user_id"];
            User_BAL data = new User_BAL();
            var details = data.UP_userLike(poll_id);
            return Json(details, JsonRequestBehavior.AllowGet);
        }

        //User Create Poll
        
        public ActionResult User_CreatePoll()
        {
            return View();
        }
        [HttpPost]
        public ActionResult User_CreatePoll(Poll p)
        {
            User_BAL cp = new User_BAL();
            var i = cp.Cratepoll_User(p);
            if(i==1)
            {
                ViewBag.Message = "Poll created Succefully";
                return RedirectToAction("Homepage", "User_Dashboard");
            }
            else
            {
                ViewBag.Message = "Poll Not created";
                return View();
            }
           
        }
        [Authorize]
        public ActionResult Edit_Poll(int id)
        {
            User_BAL p_details = new User_BAL();
            Admin_Poll_BAL data = new Admin_Poll_BAL(); 
            dynamic model = new ExpandoObject();
            model.category = data.GetCategory();
            model.poll = p_details.Selected_Polldetail(id);
            return View(model);
        }
        [HttpPost]
        public ActionResult Edit_Poll(Poll p)
        {
            User_BAL u_poll = new User_BAL();
            int i = u_poll.updatepoll_User(p);
            if(i==1)
            {
                TempData["poll_Message"] = "Poll Upadated";
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", " alert('Poll Updated Succefuly'); window.location('/UserProfile/User_MyProfile/" + p.user_id + "');", true);
                // return JavaScript("<script>alert('Poll Update Succefully');window.location=/UserProfile/User_MyProfile/"+p.user_id+"</script>");
                return RedirectToAction("User_MyProfile", "UserProfile", new { @id = p.user_id });
            }
            else
            {
                return View();
            }

        }

        public JsonResult Delete_Poll(int poll_id)
        {
            User_BAL delete_poll = new User_BAL();
            int i = delete_poll.deletepoll_User(poll_id);
            return Json(i, JsonRequestBehavior.AllowGet);

        }

        public JsonResult Close_Poll(int poll_id)
        {
            User_BAL Close_poll = new User_BAL();
            int i = Close_poll.Closepoll_User(poll_id);
            return Json(i, JsonRequestBehavior.AllowGet);

        }
        public ActionResult View_result(int id)
        {
            User_BAL p_details = new User_BAL();
            var details = p_details.Selected_Polldetail(id);
            //ViewBag.Message = msg;
            return View(details);
        }

        public JsonResult Add_Vote(Poll p)
        {
            p.user_id = (int)Session["user_id"];
            User_BAL vote = new User_BAL();
            int details1 = vote.Check_Uservote(p.user_id, p.poll_id);
            if (details1 != 3)
            {
                int details2 = vote.Add_Vote(p);
                return Json(details2, JsonRequestBehavior.AllowGet);
            }
            return Json(details1, JsonRequestBehavior.AllowGet);
        }
    }
}