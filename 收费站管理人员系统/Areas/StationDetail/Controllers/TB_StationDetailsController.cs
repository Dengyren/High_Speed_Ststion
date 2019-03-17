using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using BLL.Table;

namespace 收费站管理人员系统.Areas.StationDetail.Controllers
{
    public class TB_StationDetailsController : Controller
    {
        HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        // GET: StationDetail/TB_StationDetails
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(int? id)
        {

            ViewBag.id = id;
            return View();
        }
        public JsonResult Tolldetail(int limit, int offset, string id)

        {
            
            try
            {
                int _id = Convert.ToInt32(id);
                Table_StationDatail station_detail = new Table_StationDatail();
                var list = station_detail.Get_Toll(_id);
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
