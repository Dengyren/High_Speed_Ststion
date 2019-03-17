using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL.Table;
using BLL.UserInfoManger.Balance;
using DAL;

namespace 智能高速收费系统.Areas.UserBalance.Controllers
{
    public class View_ActionController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: UserBalance/View_Action
        public ActionResult Index()
        {
            int balance = 0;
            var result = db.TB_user.Find(Convert.ToInt32(Session["id"]));
            balance = db.TB_UserInfo.Find(result.信息编号).账户余额;
            ViewBag.balance = balance;
            ViewBag.action = Table_Balance.Get_Actionid();
            return View();
        }

        // GET: UserBalance/View_Action/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Action view_Action = db.View_Action.Find(id);
            if (view_Action == null)
            {
                return HttpNotFound();
            }
            return View(view_Action);
        }

        // GET: UserBalance/View_Action/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserBalance/View_Action/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,用户编号,操作动作,时间,当时余额,操作金额,操作id")] View_Action view_Action)
        {
            if (ModelState.IsValid)
            {
                db.View_Action.Add(view_Action);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(view_Action);
        }

        // GET: UserBalance/View_Action/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Action view_Action = db.View_Action.Find(id);
            if (view_Action == null)
            {
                return HttpNotFound();
            }
            return View(view_Action);
        }

        // POST: UserBalance/View_Action/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,用户编号,操作动作,时间,当时余额,操作金额,操作id")] View_Action view_Action)
        {
            if (ModelState.IsValid)
            {
                db.Entry(view_Action).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(view_Action);
        }

        // GET: UserBalance/View_Action/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_Action view_Action = db.View_Action.Find(id);
            if (view_Action == null)
            {
                return HttpNotFound();
            }
            return View(view_Action);
        }

        // POST: UserBalance/View_Action/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            View_Action view_Action = db.View_Action.Find(id);
            db.View_Action.Remove(view_Action);
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

        public JsonResult Table(int limit, int offset, int? ActionID)
        {
            try
            {
                int id = int.Parse(Session["id"].ToString());
                var list = Table_Balance.Get_UsreBalance(id, ActionID);
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public JsonResult recharge(string money)
        {
            User_Action.User_Recharge(int.Parse(money), Convert.ToInt32(Session["id"]));
            return Json(true);
        }

    }
}
