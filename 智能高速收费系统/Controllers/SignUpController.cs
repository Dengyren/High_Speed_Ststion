using BLL.Loginpage;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 智能高速收费系统.Controllers
{
    public class SignUpController : Controller
    {
        // GET: SignUp
        public ActionResult Index()
        {
            @ViewBag.FormAction = "/SignUp/Sign_Up";
            return View();
        }

        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public ActionResult Sign_Up(FormCollection form)
        {
            if(ModelState.IsValid)
            {
                string username = form["userID"];
                string password = form["password"];
                string userid = form["idcard"];
                if(!SignUp.UserSignup(username, password, userid))
                {
                    ViewBag.error = "该用户已被注册";
                    return View("Index");
                }
            }
            return Redirect("/Login/Index");
        }
    }
}