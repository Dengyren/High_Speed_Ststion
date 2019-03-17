using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Table;
using DAL;

namespace 交警信息管理系统.Areas.UserInfoManager.Controllers
{
    public class View_UserInfoController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: UserInfoManager/View_UserInfo
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserInfoManager/View_UserInfo/Details/5
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

        // GET: UserInfoManager/View_UserInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInfoManager/View_UserInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "身份证号码,姓名,密码,身份证照片,驾驶证号码,籍贯,手机号码,用户编号")] View_UserInfo view_UserInfo)
        {
            if (ModelState.IsValid)
            {
                db.View_UserInfo.Add(view_UserInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_UserInfo);
        }

        // GET: UserInfoManager/View_UserInfo/Edit/5
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

        // POST: UserInfoManager/View_UserInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "身份证号码,姓名,密码,身份证照片,驾驶证号码,籍贯,手机号码,用户编号")] View_UserInfo view_UserInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_UserInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_UserInfo);
        }

        // GET: UserInfoManager/View_UserInfo/Delete/5
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

        // POST: UserInfoManager/View_UserInfo/Delete/5
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

        public JsonResult Table(int limit, int offset, string id)
        {
            try
            {
                var list = Table_user.Get_UsreInfo(id);
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public JsonResult GetUserMessage(int id)
        {
            var result = from r in db.View_UserInfo
                         where (r.用户编号 == id)
                         select new
                         {
                             用户编号 = r.用户编号,
                             姓名 = r.姓名,
                             身份证号码 = r.身份证号码,
                             手机号码 = r.手机号码,
                             籍贯 = r.籍贯,
                             驾驶证号码 = r.驾驶证号码,
                             身份证照片 = r.身份证照片
                         };

            View_UserInfo TB = new View_UserInfo();
            foreach (var r in result)
            {
                TB.用户编号 = r.用户编号;
                TB.姓名 = r.姓名;
                TB.身份证号码 = r.身份证号码;
                TB.手机号码 = r.手机号码;
                TB.籍贯 = r.籍贯;
                TB.驾驶证号码 = r.驾驶证号码;
                TB.身份证照片 = "/UserCardInfo/" + id.ToString() + "/" + Path.GetFileName(r.身份证照片);
            }
            return Json(TB);
        }

    }

}
