using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.ShowMTCInfo;
using BLL.Table;
using DAL;

namespace 智能高速收费系统.Areas.User_MTC.Controllers
{
    public class TB_MTCController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: User_MTC/TB_MTC
        public ActionResult Index()
        {
            ViewBag.car = Table_MTC.Get_Car(Convert.ToInt32(Session["id"]));
            return View();
        }

        // GET: User_MTC/TB_MTC/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_MTC tB_MTC = db.TB_MTC.Find(id);
            if (tB_MTC == null)
            {
                return HttpNotFound();
            }
            return View(tB_MTC);
        }

        // GET: User_MTC/TB_MTC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User_MTC/TB_MTC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,车牌号码,进站时间,出站时间,进站点,出站点,扣费金额")] TB_MTC tB_MTC)
        {
            if (ModelState.IsValid)
            {
                db.TB_MTC.Add(tB_MTC);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_MTC);
        }

        // GET: User_MTC/TB_MTC/Edit/5
        public ActionResult Edit()
        {
            //if(id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //TB_MTC tB_MTC = db.TB_MTC.Find(id);
            //if(tB_MTC == null)
            //{
            //    return HttpNotFound();
            //}
            Table_Toll toll = new Table_Toll();
            ViewBag.toll = toll.Get_Owner();
            return View();
        }

        // POST: User_MTC/TB_MTC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "id,车牌号码,进站时间,出站时间,进站点,出站点,扣费金额")] TB_MTC tB_MTC)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(tB_MTC).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    return View(tB_MTC);
        //}

        // GET: User_MTC/TB_MTC/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_MTC tB_MTC = db.TB_MTC.Find(id);
            if (tB_MTC == null)
            {
                return HttpNotFound();
            }
            return View(tB_MTC);
        }

        // POST: User_MTC/TB_MTC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_MTC tB_MTC = db.TB_MTC.Find(id);
            db.TB_MTC.Remove(tB_MTC);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult Table(int limit, int offset, string CarID,string tollName)
        {
            try
            {
                int id = int.Parse(Session["id"].ToString());
                var list = Table_MTC.Get_UsreMTC(id, CarID,tollName);
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public JsonResult ShowPath(string id)
        {
            try
            {
                int _id = int.Parse(id);
                GetCarPath GP = new GetCarPath();
                List<string> title = GP.getMsg(_id);

                return Json(title);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        public JsonResult TableToll(int limit, int offset,string station,string place)
        {
            try
            {
                Table_Toll toll = new Table_Toll();
                var list = toll.Get_UserToll(station, place);
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}
