using BLL.UserPage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 智能高速收费系统.Areas.Home.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Home
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetMsg()
        {
            UserHomePage UHP = new UserHomePage();
            Dictionary<string, int> dic = UHP.GetUserMsg(Convert.ToInt32(Session["id"]));
            return Json(dic);
        }





    }
}