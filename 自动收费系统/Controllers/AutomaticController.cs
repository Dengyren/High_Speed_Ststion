using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using 自动收费系统.Randomfunction;


namespace 自动收费系统.Controllers
{
    public class AutomaticController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        // GET: Automatic
        public ActionResult Index(string BtnSubmit)
        {
            RandomObject roj = new RandomObject();
            TB_MTC tB_MTC = new TB_MTC();
            if (BtnSubmit == "模拟进站")
            {
                tB_MTC.车牌号码 = "粤T"+roj.RandCode(5);
                tB_MTC.进站时间 = DateTime.Now;
                tB_MTC.进站点 = "A";
                tB_MTC.出站点 = "";
            }
            else if (BtnSubmit == "模拟出站")
            {
                tB_MTC.车牌号码 = "粤A"+roj.RandCode(5);
                tB_MTC.进站时间 = DateTime.Now;
                tB_MTC.进站点 = "A";
                tB_MTC.出站点 = "";
                Thread.Sleep(2000);
                tB_MTC.出站时间 = DateTime.Now;
            }
            db.TB_MTC.Add(tB_MTC);
            db.SaveChanges();
            

            return View("Index");
        }
    }
}