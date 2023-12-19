using Poll4u_Web_BAL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Poll4u_Web_BOL;

namespace Poll4u_Web.Controllers
{
    [Authorize]
    public class ManageUserController : Controller
    {
        public ActionResult GetAllUsers()
        {
            Admin_User_BAL data = new Admin_User_BAL();
            var details = data.Get_AllUser();
            return View(details);
        }

        public ActionResult GetUserDetail(int id)
        {
            Admin_User_BAL data = new Admin_User_BAL();
            Admin_Poll_BAL data2 = new Admin_Poll_BAL();
            dynamic model = new ExpandoObject();
            model.User = data.Details_User(id);
            model.Poll = data2.Get_UserPoll(id);
            return View(model);
        }
        public ActionResult GetRemovedUsers()
        {
            Admin_User_BAL data = new Admin_User_BAL();
            var details = data.Get_RemovedUser();
            return View(details);
        }

        public ActionResult RemoveUser(int id)
        {
            Admin_User_BAL data = new Admin_User_BAL();
            data.RemoveUser(id);
            return RedirectToAction("GetAllUsers", "ManageUser");
        }
        public ActionResult Unbanuser(int id)
        {
            Admin_User_BAL data = new Admin_User_BAL();
            data.unbanUser(id);
            return RedirectToAction("GetRemovedUsers", "ManageUser");
        }
    }
}