using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAL;

namespace 后台管理系统.Areas.UserFeedBack.Controllers
{
    public class TB_FeedBackController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: UserFeedBack/TB_FeedBack
        public ActionResult Index()
        {
            return View(db.TB_FeedBack.ToList());
        }

        // GET: UserFeedBack/TB_FeedBack/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FeedBack tB_FeedBack = db.TB_FeedBack.Find(id);
            if(tB_FeedBack == null)
            {
                return HttpNotFound();
            }
            return View(tB_FeedBack);
        }

        // GET: UserFeedBack/TB_FeedBack/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserFeedBack/TB_FeedBack/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,日期,用户编号,反馈内容,反馈类型,状态")] TB_FeedBack tB_FeedBack)
        {
            if(ModelState.IsValid)
            {
                db.TB_FeedBack.Add(tB_FeedBack);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_FeedBack);
        }

        // GET: UserFeedBack/TB_FeedBack/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FeedBack tB_FeedBack = db.TB_FeedBack.Find(id);
            if(tB_FeedBack == null)
            {
                return HttpNotFound();
            }
            return View(tB_FeedBack);
        }

        // POST: UserFeedBack/TB_FeedBack/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,日期,用户编号,反馈内容,反馈类型,状态")] TB_FeedBack tB_FeedBack)
        {
            if(ModelState.IsValid)
            {
                db.Entry(tB_FeedBack).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_FeedBack);
        }

        // GET: UserFeedBack/TB_FeedBack/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_FeedBack tB_FeedBack = db.TB_FeedBack.Find(id);
            if(tB_FeedBack == null)
            {
                return HttpNotFound();
            }
            return View(tB_FeedBack);
        }

        // POST: UserFeedBack/TB_FeedBack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_FeedBack tB_FeedBack = db.TB_FeedBack.Find(id);
            db.TB_FeedBack.Remove(tB_FeedBack);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }



        public JsonResult Table(int limit, int offset, string id, string kindid)
        {
            try
            {
                var list = db.TB_FeedBack.Where(c => ((c.反馈类型.Contains(kindid)) && (c.状态.Contains(id)))).OrderByDescending(c => c.id).OrderBy(p => p.状态).ToList();
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }


        [HttpPost]
        public JsonResult GetMsg(int id)
        {
            TB_FeedBack tb = db.TB_FeedBack.Find(id);
            return Json(tb);
        }

        public JsonResult NewEdit(string state, int id)
        {
            try
            {
                TB_FeedBack tb = db.TB_FeedBack.Find(id);
                tb.状态 = state;
                if(ModelState.IsValid)
                {
                    db.Entry(tb).State = EntityState.Modified;
                    db.SaveChanges();
                }
                return Json(true);
            }
            catch(Exception e )
            {
                throw new Exception(e.Message);
            }


        }
    }
}
