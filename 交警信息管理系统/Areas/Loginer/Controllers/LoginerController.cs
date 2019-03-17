using BLL.Loginpage;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 交警信息管理系统.Areas.Loginer.Controllers
{
    public class LoginerController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: Loginer/Loginer
        public ActionResult Index()
        {
            ViewBag.count = "/InfoAduit/InfoAudit/getInfoCount";
            ViewBag.FormAction = "Loginer/Loginer/CheckPower";
            return View();
        }


        public ActionResult CheckPower(FormCollection form)
        {
            TB_pcUser c = Login.pcUser_Login(form["userID"], form["password"]);
            if (c != null)
            {
                Session["pcID"] = c.警员编号;
                Session["pcName"] = c.姓名;
                return RedirectToAction("Index","../Home/Home");
            }
            else
            {
                ViewBag.error = "error";
                return View("Index");
            }
        }

    }
}