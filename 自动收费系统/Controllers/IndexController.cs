using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using 自动收费系统.Random_fanction;

namespace 自动收费系统.Controllers
{
    public class IndexController : Controller
    {
        public static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        RandomObject randomObject = new RandomObject();
        // GET: Index
        public ActionResult Index(string BtnSubmit)
        {
            try
            {
                switch (BtnSubmit)
                {
                    case "模拟类型1":
                        for (int i = 0; i < 5; i++)
                        {
                            TB_MTC tB_MTC = new TB_MTC
                            {
                                车牌号码 = "粤T" + randomObject.RandCode(5),
                                进站时间 = DateTime.Now.ToString(),
                                进站点 = "A",
                                出站时间 = DateTime.Now.ToString(),
                                出站点 = "B",
                            };
                            db.TB_MTC.Add(tB_MTC);
                        }
                        db.SaveChanges();
                        return Json(true);
                    case "模拟类型2":
                        for (int i = 0; i < 5; i++)
                        {
                            TB_MTC tB_MTC = new TB_MTC
                            {
                                车牌号码 = "粤T" + randomObject.RandCode(5),
                                进站时间 = DateTime.Now.ToString(),
                                进站点 = "A",
                            };
                            db.TB_MTC.Add(tB_MTC);
                        }
                        db.SaveChanges();
                        return Json(true);
                }
                return View("Index");
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }



        }
        public JsonResult GetDepartment(int limit, int offset, string departmentname, string statu)
        {
            List<TB_MTC> tb = db.TB_MTC.ToList();

            var total = tb.Count;
            var rows = tb.Skip(offset).Take(limit).ToList();
            return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
        }
    }
}