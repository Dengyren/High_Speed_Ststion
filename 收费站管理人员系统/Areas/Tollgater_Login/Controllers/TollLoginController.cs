using BLL.Loginpage;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace 收费站管理人员系统.Areas.Tollgater_Login.Controllers
{
    public class TollLoginController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        // GET: Tollgater_Login/TollLogin
        public ActionResult Index()
        {
            ViewBag.FormAction = "/TollLogin/CheckPower";
            return View();
        }
        public ActionResult CheckPower(FormCollection form)
        {
            TB_StationManager c = Login.StationManager_Login(form["userID"], form["password"]);
            if (c != null)
            {
                Session["name"] = c.姓名;
                Session["id"] = c.管理员编号;
                Session["管区id"] = c.管区id;
                //if (Login.First_Sign_In(c))
                //    return RedirectToAction("Create", "UserInfo/View_UserInfo");
                return RedirectToAction("Index", "../Home/Home");
            }
            else
            {
                ViewBag.error = "用户名不存在或密码错误";
                ViewBag.FormAction = "CheckPower";
                return View("Index");
            }
        }
    }
}