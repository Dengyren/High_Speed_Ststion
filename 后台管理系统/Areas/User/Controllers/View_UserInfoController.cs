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

namespace 后台管理系统.Areas.User.Controllers
{
    public class View_UserInfoController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: User/View_UserInfo
        public ActionResult Index()
        {
            return View(db.View_UserInfo.ToList());
        }

        // GET: User/View_UserInfo/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_UserInfo view_UserInfo = db.View_UserInfo.Find(id);
            if (view_UserInfo == null)
            {
                return HttpNotFound();
            }
            return View(view_UserInfo);
        }

        // GET: User/View_UserInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/View_UserInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "身份证号码,姓名,身份证照片,驾驶证号码,籍贯,手机号码,用户编号")] View_UserInfo view_UserInfo)
        {
            if (ModelState.IsValid)
            {
                db.View_UserInfo.Add(view_UserInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_UserInfo);
        }

        // GET: User/View_UserInfo/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_UserInfo view_UserInfo = db.View_UserInfo.Find(id);
            if (view_UserInfo == null)
            {
                return HttpNotFound();
            }
            return View(view_UserInfo);
        }

        // POST: User/View_UserInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "身份证号码,姓名,身份证照片,驾驶证号码,籍贯,手机号码,用户编号")] View_UserInfo view_UserInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_UserInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_UserInfo);
        }

        // GET: User/View_UserInfo/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_UserInfo view_UserInfo = db.View_UserInfo.Find(id);
            if (view_UserInfo == null)
            {
                return HttpNotFound();
            }
            return View(view_UserInfo);
        }

        // POST: User/View_UserInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            View_UserInfo view_UserInfo = db.View_UserInfo.Find(id);
            db.View_UserInfo.Remove(view_UserInfo);
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

        public JsonResult Table(int limit, int offset, string id,string name)
        {
            try
            {
                var list = Table_user.BackGet_UsreInfo(id,name);
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
