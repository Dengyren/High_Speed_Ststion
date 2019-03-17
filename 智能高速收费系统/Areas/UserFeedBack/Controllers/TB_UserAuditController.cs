using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace 智能高速收费系统.Areas.UserFeedBack.Controllers
{
    public class TB_UserAuditController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: UserFeedBack/TB_UserAudit
        public ActionResult Index()
        {
            ViewBag.action = GetAction();
            return View();
        }

        // GET: UserFeedBack/TB_UserAudit/Details/5
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

        // GET: UserFeedBack/TB_UserAudit/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserFeedBack/TB_UserAudit/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,用户编号,身份证号码,姓名,驾驶证号码,籍贯,手机号码,身份证照片,状态编号")] TB_UserAudit tB_UserAudit)
        {
            if (ModelState.IsValid)
            {
                db.TB_UserAudit.Add(tB_UserAudit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_UserAudit);
        }

        // GET: UserFeedBack/TB_UserAudit/Edit/5
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

        // POST: UserFeedBack/TB_UserAudit/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,用户编号,身份证号码,姓名,驾驶证号码,籍贯,手机号码,身份证照片,状态编号")] TB_UserAudit tB_UserAudit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tB_UserAudit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_UserAudit);
        }

        // GET: UserFeedBack/TB_UserAudit/Delete/5
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

        // POST: UserFeedBack/TB_UserAudit/Delete/5
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

        public JsonResult Table(int limit, int offset, string ActionID)
        {
            try
            {
                int id = int.Parse(Session["id"].ToString());
                var list = db.View_UserFeedBackAudit.Where(c => c.用户编号 == id&&c.状态.Contains(ActionID)).OrderByDescending(c=>c.id).ToList();
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Dictionary<string ,string> GetAction()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var result = db.TB_AuditStatus.ToList();
            foreach(var r in result)
            {
                if(r.id!=4)
                    dic.Add(r.状态, r.状态);

            }
            return dic;
        }

    }
}
