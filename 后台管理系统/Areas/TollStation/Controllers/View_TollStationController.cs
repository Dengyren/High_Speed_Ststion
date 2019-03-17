using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace 后台管理系统.Areas.TollStation.Controllers
{
    public class View_TollStationController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: TollStation/View_TollStation
        public ActionResult Index()
        {
            return View(db.View_TollStation.ToList());
        }

        // GET: TollStation/View_TollStation/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_TollStation view_TollStation = db.View_TollStation.Find(id);
            if (view_TollStation == null)
            {
                return HttpNotFound();
            }
            return View(view_TollStation);
        }

        // GET: TollStation/View_TollStation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TollStation/View_TollStation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "归属管区,id,站点名称,收费金额")] View_TollStation view_TollStation)
        {
            if (ModelState.IsValid)
            {
                db.View_TollStation.Add(view_TollStation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_TollStation);
        }

        // GET: TollStation/View_TollStation/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_TollStation view_TollStation = db.View_TollStation.Find(id);
            if (view_TollStation == null)
            {
                return HttpNotFound();
            }
            return View(view_TollStation);
        }

        // POST: TollStation/View_TollStation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "归属管区,id,站点名称,收费金额")] View_TollStation view_TollStation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_TollStation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_TollStation);
        }

        // GET: TollStation/View_TollStation/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_TollStation view_TollStation = db.View_TollStation.Find(id);
            if (view_TollStation == null)
            {
                return HttpNotFound();
            }
            return View(view_TollStation);
        }

        // POST: TollStation/View_TollStation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            View_TollStation view_TollStation = db.View_TollStation.Find(id);
            db.View_TollStation.Remove(view_TollStation);
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


        public JsonResult Table(int limit, int offset, string id,string Name)
        {
            try
            {
                var list = db.View_TollStation.Where(c => c.站点名称.Contains(Name) && c.归属管区.Contains(id)).ToList();
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
