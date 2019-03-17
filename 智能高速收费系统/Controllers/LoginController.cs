using BLL.Loginpage;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 智能高速收费系统.Controllers
{
    public class LoginController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        // GET: Login
        public ActionResult Index()
        { 
            ViewBag.FormAction = "/Login/CheckPower";
            Session.Abandon();//清除全部Session
            return View();
        }


        public ActionResult CheckPower(FormCollection form)
        {
            TB_user c = Login.User_Login(form["userID"], form["password"]);
            if(c != null)
            {
                Session["name"] = c.姓名;
                Session["id"] = c.用户编号;
                if(Login.First_Sign_In(c))
                    return RedirectToAction("Create", "UserInfo/View_UserInfo");
                return RedirectToAction("Index", "Home/Home");
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