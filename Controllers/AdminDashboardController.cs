using Poll4u_Web_BAL;
using Poll4u_Web_BOL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poll4u_Web.Controllers
{
    [Authorize]
    public class AdminDashboardController : Controller
    {
        string str = ConfigurationManager.ConnectionStrings["Poll4u_Web"].ConnectionString;
        // GET: Dashboard
        public ActionResult Report()
        {
            Admin_User_BAL Uchart = new Admin_User_BAL();
            Admin_Poll_BAL Pchart = new Admin_Poll_BAL();
            Admin_User_BAL Userdetail = new Admin_User_BAL();
            Admin_Poll_BAL PollDetail = new Admin_Poll_BAL();
            dynamic model = new ExpandoObject();
            model.ChartUser = Uchart.UserChart();
            model.ChartPoll = Pchart.PollChart();
            model.User = Userdetail.Get_LastweekUser();
            model.Poll = PollDetail.Get_LastWeekPolls();
            return View(model);
        }

    }
}