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

namespace 交警信息管理系统.Areas.policeManager.Controllers
{
    public class TB_pcUserController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: policeManager/TB_pcUser
        public ActionResult Index()
        {
            return View(db.TB_pcUser.ToList());
        }

        // GET: policeManager/TB_pcUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_pcUser tB_pcUser = db.TB_pcUser.Find(id);
            if (tB_pcUser == null)
            {
                return HttpNotFound();
            }
            return View(tB_pcUser);
        }

        // GET: policeManager/TB_pcUser/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: policeManager/TB_pcUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,警员编号,密码")] TB_pcUser tB_pcUser)
        {
            if (ModelState.IsValid)
            {
                db.TB_pcUser.Add(tB_pcUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_pcUser);
        }

        // GET: policeManager/TB_pcUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_pcUser tB_pcUser = db.TB_pcUser.Find(id);
            if (tB_pcUser == null)
            {
                return HttpNotFound();
            }
            return View(tB_pcUser);
        }

        // POST: policeManager/TB_pcUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,警员编号,密码")] TB_pcUser tB_pcUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_pcUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_pcUser);
        }

        // GET: policeManager/TB_pcUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_pcUser tB_pcUser = db.TB_pcUser.Find(id);
            if (tB_pcUser == null)
            {
                return HttpNotFound();
            }
            return View(tB_pcUser);
        }

        // POST: policeManager/TB_pcUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_pcUser tB_pcUser = db.TB_pcUser.Find(id);
            db.TB_pcUser.Remove(tB_pcUser);
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

        public JsonResult Table(int limit, int offset, string id, string Name)
        {
            try
            {
                var list = Table_PCuser.Get_PCuser(id, Name);
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
