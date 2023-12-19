using Poll4u_Web_BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Poll4u_Web_BAL;
using System.Dynamic;

namespace Poll4u_Web.Controllers
{
    [Authorize]
    public class PollController : Controller
    {
        // GET: Poll

        public ActionResult AllPolls()
        {
            Admin_Poll_BAL data = new Admin_Poll_BAL();
            var details = data.Get_AllPolls();
            return View(details);
        }
        public ActionResult Expire_ClosePolls()
        {
            Admin_Poll_BAL data = new Admin_Poll_BAL();
            var details = data.Get_ExpireClosePolls();
            return View(details);
        }
        public ActionResult PollDetail(int p_id, int u_id)
        {
            Admin_Poll_BAL data = new Admin_Poll_BAL();
            Admin_User_BAL data2 = new Admin_User_BAL();
            User_BAL data3 = new User_BAL();
            Admin_Poll_BAL data4 = new Admin_Poll_BAL();
            dynamic model = new ExpandoObject();
            model.Poll = data.Get_PollDetail(p_id);
            model.User = data2.Details_User(u_id);
            model.Comment = data3.Get_UserComment(p_id);
            model.Like = data4.Get_UserPollLikes(p_id);
            return View(model);
        }

        public ActionResult RemovedPolls(int id)
        {
            Poll4u_Web_BAL.Admin_Poll_BAL data = new Poll4u_Web_BAL.Admin_Poll_BAL();
            data.RemovePoll(id);
            return RedirectToAction("AllPolls", "Poll");
        }

        public ActionResult UnbanPolls(int id)
        {
            Poll4u_Web_BAL.Admin_Poll_BAL data = new Poll4u_Web_BAL.Admin_Poll_BAL();
            data.UnbanPoll(id);
            return RedirectToAction("Get_RemovedPolls", "Poll");
        }

        public ActionResult Get_RemovedPolls()
        {
            Poll4u_Web_BAL.Admin_Poll_BAL data = new Poll4u_Web_BAL.Admin_Poll_BAL();
            var details = data.Get_RemovdePoll();
            return View(details);
        }

        public ActionResult Get_Category()
        {
            Poll4u_Web_BAL.Admin_Poll_BAL data = new Poll4u_Web_BAL.Admin_Poll_BAL();
            var details = data.GetCategory();
            return View(details);
        }
        public ActionResult Add_Category(string Category)
        {
            Admin_Poll_BAL data = new Admin_Poll_BAL();
            int r=data.AddCategory(Category);
            if (r == 1)
            {
                TempData["Category"] = "Category Added Succefully";
            }
            else
            {
                TempData["Category"] = "Category Not Added";
            }
            return RedirectToAction("Get_Category", "Poll");
        }

        public ActionResult Remove_Category(int id)
        {
            Admin_Poll_BAL Remove = new Admin_Poll_BAL();
            Remove.RemoveCategory(id);
            return RedirectToAction("Get_Category", "Poll");
        }
        public ActionResult Create_Poll ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create_Poll(Poll p)
        {
            Admin_Poll_BAL createpoll = new Admin_Poll_BAL();
            int i = createpoll.AdmincreatePoll(p);
            if(i==1)
            {
                ViewBag.Message = "Poll Created Succefully"; 
            }
            else
            {
                ViewBag.Message = "Poll Not Created";

            }
            return View();
          
        }

        public ActionResult AdminPoll()
        {
            Poll4u_Web_BAL.Admin_Poll_BAL data = new Poll4u_Web_BAL.Admin_Poll_BAL();
            dynamic model = new ExpandoObject();
            model.Poll = data.Get_AdminPolls();
            model.Pollcomment = data.Get_AdminPollComments();
            model.Like = data.Get_AdminPollLikes();
            return View(model);
        }
        public ActionResult AdminDeletePolls(int id)
        {
            Admin_Poll_BAL data = new Admin_Poll_BAL();
            data.DeleteAdmin_Poll(id);
            return RedirectToAction("AdminPoll", "Poll");
        }

    }
}