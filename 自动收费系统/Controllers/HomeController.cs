using BLL.Actomatic_Deduction;
using BLL.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 自动收费系统.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult create()
        {
            GetRandomCar car = new GetRandomCar();
            HighSpeedCarManger manger = new HighSpeedCarManger();
            string carName = car.Getcarid() ;
            while(car.checkCar(carName))
            {
                carName = car.Getcarid();
            }
            manger.CreateCarMsg(carName);
            return Json(carName);
        }

        public JsonResult addMidStation()
        {
            GetHighSpeedCar GHS = new GetHighSpeedCar();
            HighSpeedCarManger manger = new HighSpeedCarManger();
            int id = GHS.Get_HighSpeedCar();
            if(id == 0)
                return Json(false);
            manger.AddStationMsg(id);
            return Json(id);
        }

        public JsonResult ExitStation()
        {
            GetHighSpeedCar GHS = new GetHighSpeedCar();
            HighSpeedCarManger manger = new HighSpeedCarManger();
            int id = GHS.Get_HighSpeedCar();
            if(id == 0)
                return Json(false);
            manger.ExitStationMsg(id);
            return Json(id);
        }

        public JsonResult ShowPath(int id)
        {
            ShowFullPath SFP = new ShowFullPath();
            Dictionary<string, string> dic = SFP.Show(id);
            return Json(dic);
        }

        public JsonResult Table1(int limit, int offset)
        {
            try
            {
                Table_HighSpeed tb = new Table_HighSpeed();
                var list = tb.GetInMsg();
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public JsonResult Table2(int limit, int offset)
        {
            try
            {
                Table_HighSpeed tb = new Table_HighSpeed();
                var list = tb.GetMidMsg();
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public JsonResult Table3(int limit, int offset)
        {
            try
            {
                Table_HighSpeed tb = new Table_HighSpeed();
                var list = tb.GetExitMsg();
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}