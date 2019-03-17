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
using BLL.UserInfoManger;
using BLL.UserInfoManger.Car;
using BLL.UserInfoManger.Examine_message;
using DAL;

namespace 智能高速收费系统.Areas.UserInfo.Controllers
{
    public class TB_carIDController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: UserInfo/TB_carID
        public ActionResult Index()
        {
            return View(db.TB_carID.ToList());
        }

        // GET: UserInfo/TB_carID/Details/5
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_carID tB_carID = db.TB_carID.Find(id);
            if(tB_carID == null)
            {
                return HttpNotFound();
            }
            return View(tB_carID);
        }

        // GET: UserInfo/TB_carID/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserInfo/TB_carID/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,车牌号,用户编号,状态编号,车牌照片前,车牌照片后")] TB_carID tB_carID)
        {
            if(ModelState.IsValid)
            {
                db.TB_carID.Add(tB_carID);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tB_carID);
        }

        // GET: UserInfo/TB_carID/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_carID tB_carID = db.TB_carID.Find(id);
            if(tB_carID == null)
            {
                return HttpNotFound();
            }
            tB_carID.车牌照片前 = "/UserCardInfo/" + Session["id"].ToString() + "/" + Path.GetFileName(tB_carID.车牌照片前);
            tB_carID.车牌照片后 = "/UserCardInfo/" + Session["id"].ToString() + "/" + Path.GetFileName(tB_carID.车牌照片后);
            return View(tB_carID);
        }

        // POST: UserInfo/TB_carID/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,车牌号,用户编号,状态编号,车牌照片前,车牌照片后")] TB_carID tB_carID)
        {
            if(ModelState.IsValid)
            {
                db.Entry(tB_carID).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tB_carID);
        }

        // GET: UserInfo/TB_carID/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TB_carID tB_carID = db.TB_carID.Find(id);
            if(tB_carID == null)
            {
                return HttpNotFound();
            }
            return View(tB_carID);
        }

        // POST: UserInfo/TB_carID/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TB_carID tB_carID = db.TB_carID.Find(id);
            db.TB_carID.Remove(tB_carID);
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

        //用于创建车辆信息

        public ActionResult Create_NewCar(string CarID, string F_photo, string B_photo)
        {
            if(ModelState.IsValid)
            {
                string path = Server.MapPath("~/");
                F_photo = FileUpload.SaveReadyInfo(F_photo, path, Session["id"].ToString());
                B_photo = FileUpload.SaveReadyInfo(B_photo, path, Session["id"].ToString());
                CarManager.Create_CarInfo(Convert.ToInt32(Session["id"]), CarID, F_photo, B_photo);
                return Json(true);
            }
            return Json(false);
        }

        //展示车辆信息
        public JsonResult Table(int limit, int offset)
        {
            try
            {
                int id = int.Parse(Session["id"].ToString());
                var list = Table_car.Get_UsreCar(id);
                var total = list.Count;
                var rows = list.Skip(offset).Take(limit).ToList();
                return Json(new { total = total, rows = rows }, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        //用于上传车辆照片
        public JsonResult Carphoto_Import1(HttpPostedFileBase file, string carID)
        {
            try
            {
                string path = Server.MapPath("~/");
                string name = file.FileName;
                string photo_path = FileUpload.SaveInfo(file, path, carID);

                return Json(photo_path);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [HttpPost]
        public JsonResult GetDelCar(int id)
        {
            try
            {
                List<string> list = new List<string>();
                list = GetUserCarInfo.GetDelete_CarInfo(id, Session["id"].ToString());
                return Json(list);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public ActionResult DelCar(int id)
        {
            CarManager.Delete_CarInfo(id);
            return Json(true);
        }

        public ActionResult EditCar(TB_carID msg)
        {
            if(ModelState.IsValid)
            {
                string path = Server.MapPath("~/");
                CarManager.Edit_CarInfo(msg, path, Session["id"].ToString());
                return Json(true);
            }
            return Json(false);
        }

        public JsonResult GetExamineMsg()
        {
            User_Examine user = new User_Examine();
            ViewData.Model = user.Find(Convert.ToInt32(Session["id"]));
            return Json(ViewData.Model);
        }



        public JsonResult SendExamineMsg()
        {
            try
            {
                Send_Examine user = new Send_Examine();
                user.Send(Convert.ToInt32(Session["id"]));
                return Json(true);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
