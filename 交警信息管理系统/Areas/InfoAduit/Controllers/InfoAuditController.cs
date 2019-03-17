using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.AuditManager;
using BLL.Table;
using DAL;

namespace 交警信息管理系统.Areas.InfoAduit.Controllers
{
    public class InfoAuditController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: InfoAduit/InfoAudit
        public ActionResult Index()
        {   
            return View(db.TB_UserAudit.ToList());
        }

        // GET: InfoAduit/InfoAudit/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_UserAudit tB_UserAudit = db.TB_UserAudit.Find(id);
            if (tB_UserAudit == null)
            {
                return HttpNotFound();
            }
            return View(tB_UserAudit);
        }

        // GET: InfoAduit/InfoAudit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: InfoAduit/InfoAudit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,用户编号,身份证号码,姓名,驾驶证号码,籍贯,手机号码")] TB_UserAudit tB_UserAudit)
        {
            if (ModelState.IsValid)
            {
                db.TB_UserAudit.Add(tB_UserAudit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_UserAudit);
        }

        // GET: InfoAduit/InfoAudit/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_UserAudit tB_UserAudit = db.TB_UserAudit.Find(id);
            if (tB_UserAudit == null)
            {
                return HttpNotFound();
            }
            return View(tB_UserAudit);
        }

        // POST: InfoAduit/InfoAudit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,用户编号,身份证号码,姓名,驾驶证号码,籍贯,手机号码")] TB_UserAudit tB_UserAudit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_UserAudit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_UserAudit);
        }

        // GET: InfoAduit/InfoAudit/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_UserAudit tB_UserAudit = db.TB_UserAudit.Find(id);
            if (tB_UserAudit == null)
            {
                return HttpNotFound();
            }
            return View(tB_UserAudit);
        }

        // POST: InfoAduit/InfoAudit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_UserAudit tB_UserAudit = db.TB_UserAudit.Find(id);
            db.TB_UserAudit.Remove(tB_UserAudit);
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

        public JsonResult Table(int limit, int offset, string id)
        {
            try
            {
                var list = Table_AuditInfo.Get_UserInfo();
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public JsonResult GetExamineMsg(string id)
        {
            getAudit_Info user = new getAudit_Info();
            ViewData.Model = user.Find(Convert.ToInt32(id));
            return Json(ViewData.Model);
        }

        public JsonResult SendPassMsg(string id)
        {
            try
            {
                setAudit_Info user = new setAudit_Info();
                user.SendPass(Convert.ToInt32(id));
                return Json(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public JsonResult SendFailMsg(string id)
        {
            try
            {
                setAudit_Info user = new setAudit_Info();
                user.SendFail(Convert.ToInt32(id));
                return Json(true);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
