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

namespace 交警信息管理系统.Areas.CarInfoManager.Controllers
{
    public class View_CarInfoController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: CarInfoManager/View_CarInfo
        public ActionResult Index()
        {
            return View(db.View_CarInfo.ToList());
        }

        // GET: CarInfoManager/View_CarInfo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_CarInfo view_CarInfo = db.View_CarInfo.Find(id);
            if (view_CarInfo == null)
            {
                return HttpNotFound();
            }
            return View(view_CarInfo);
        }

        // GET: CarInfoManager/View_CarInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CarInfoManager/View_CarInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,用户编号,姓名,身份证号码,车牌号,状态编号,车牌照片后,车牌照片前")] View_CarInfo view_CarInfo)
        {
            if (ModelState.IsValid)
            {
                db.View_CarInfo.Add(view_CarInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_CarInfo);
        }

        // GET: CarInfoManager/View_CarInfo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_CarInfo view_CarInfo = db.View_CarInfo.Find(id);
            if (view_CarInfo == null)
            {
                return HttpNotFound();
            }
            return View(view_CarInfo);
        }

        // POST: CarInfoManager/View_CarInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,用户编号,姓名,身份证号码,车牌号,状态编号,车牌照片后,车牌照片前")] View_CarInfo view_CarInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_CarInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_CarInfo);
        }

        // GET: CarInfoManager/View_CarInfo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_CarInfo view_CarInfo = db.View_CarInfo.Find(id);
            if (view_CarInfo == null)
            {
                return HttpNotFound();
            }
            return View(view_CarInfo);
        }

        // POST: CarInfoManager/View_CarInfo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            View_CarInfo view_CarInfo = db.View_CarInfo.Find(id);
            db.View_CarInfo.Remove(view_CarInfo);
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

        public JsonResult Table(int limit, int offset, string id,string CarId,string Name)
        {
            try
            {
                var list = Table_CarMessage.Get_CarInfo(id,CarId,Name);
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public JsonResult GetCarMessage(int id)
        {
            var result = from r in db.View_CarInfo
                         where (r.id == id)
                         select new
                         {
                             r.id,
                             r.用户编号,
                             r.姓名,
                             r.身份证号码,
                             r.车牌号,
                             r.状态编号,
                             r.车牌照片前,
                             r.车牌照片后
                         };

            View_CarInfo TB = new View_CarInfo();
            foreach (var r in result)
            {
                TB.id = r.id;
                TB.用户编号 = r.用户编号;
                TB.姓名 = r.姓名;
                TB.身份证号码 = r.身份证号码;
                TB.车牌号 = r.车牌号;
                TB.状态编号 = r.状态编号;
                TB.车牌照片前 = "/UserCardInfo/" + r.用户编号.ToString() + "/" + Path.GetFileName(r.车牌照片前);
                TB.车牌照片后 = "/UserCardInfo/" + r.用户编号.ToString() + "/" + Path.GetFileName(r.车牌照片后);
            }
            return Json(TB);
        }
    }
}
