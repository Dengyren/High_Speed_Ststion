using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Table;
using DAL;

namespace 收费站管理人员系统.Areas.StationAdmin.Controllers
{
    public class TB_TollgateController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: StationAdmin/TB_Tollgate
        public ActionResult Index()
        {
            return View(db.TB_Tollgate.ToList());
        }

        // GET: StationAdmin/TB_Tollgate/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var view_StationDetail = db.View_StationDetail.Where(c => c.id == id).Distinct();
        //    if (view_StationDetail == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(view_StationDetail);
        //}

        // GET: StationAdmin/TB_Tollgate/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StationAdmin/TB_Tollgate/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,站点名称,收费金额,归属管区id")] TB_Tollgate tB_Tollgate)
        {
            if (ModelState.IsValid)
            {
                db.TB_Tollgate.Add(tB_Tollgate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_Tollgate);
        }

        // GET: StationAdmin/TB_Tollgate/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_Tollgate tB_Tollgate = db.TB_Tollgate.Find(id);
            if (tB_Tollgate == null)
            {
                return HttpNotFound();
            }
            return View(tB_Tollgate);
        }

        // POST: StationAdmin/TB_Tollgate/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,站点名称,收费金额,归属管区id")] TB_Tollgate tB_Tollgate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_Tollgate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_Tollgate);
        }

        // GET: StationAdmin/TB_Tollgate/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_Tollgate tB_Tollgate = db.TB_Tollgate.Find(id);
            if (tB_Tollgate == null)
            {
                return HttpNotFound();
            }
            return View(tB_Tollgate);
        }

        // POST: StationAdmin/TB_Tollgate/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_Tollgate tB_Tollgate = db.TB_Tollgate.Find(id);
            db.TB_Tollgate.Remove(tB_Tollgate);
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

        public JsonResult Table(int limit, int offset,string id)
             
        {
            try
            {
                var area = Session["管区id"];
                Table_Toll toll = new Table_Toll();
                var list = toll.Get_Toll(id,Convert.ToInt32(area));
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
