
using BLL.UserInfoManger;
using BLL.UserInfoManger.Examine_message;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace 智能高速收费系统.Areas.UserInfo.Controllers
{
    public class View_UserInfoController : Controller
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        // GET: UserInfo/View_UserInfo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Index1()
        {
            return View();
        }

        // GET: UserInfo/View_UserInfo/Details/5
        public ActionResult Details(string id)
        {
            //if(id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //View_UserInfo view_UserInfo = db.View_UserInfo.Find(id);
            //if(view_UserInfo == null)
            //{
            //    return HttpNotFound();
            //}
            return View();
        }

        // GET: UserInfo/View_UserInfo/Create
        public ActionResult Create()
        {

            return View();
        }

        // POST: UserInfo/View_UserInfo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "身份证号码,姓名,密码,手机号码,籍贯,驾驶证号码,身份证照片,账户余额")] View_UserInfo view_UserInfo)
        {
            if(ModelState.IsValid)
            {
                String idStr = Session["id"].ToString();
                int id = int.Parse(idStr);
                CreateUserInfo.Create_UserInfo(view_UserInfo, id);
                return RedirectToAction("Index");
            }
            return View(view_UserInfo);
        }

        // GET: UserInfo/View_UserInfo/Edit/5
        public ActionResult Edit(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_UserInfo view_UserInfo = db.View_UserInfo.Find(id);
            if(view_UserInfo == null)
            {
                return HttpNotFound();
            }
            return View(view_UserInfo);
        }

        // POST: UserInfo/View_UserInfo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "身份证号码,姓名,密码,手机号码,籍贯,驾驶证号码,身份证照片,账户余额")] View_UserInfo view_UserInfo)
        {
            if(ModelState.IsValid)
            {
                db.Entry(view_UserInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Home/Home");
            }
            return View(view_UserInfo);
        }

        // GET: UserInfo/View_UserInfo/Delete/5
        public ActionResult Delete(string id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            View_UserInfo view_UserInfo = db.View_UserInfo.Find(id);
            if(view_UserInfo == null)
            {
                return HttpNotFound();
            }
            return View(view_UserInfo);
        }

        // POST: UserInfo/View_UserInfo/Delete/5
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
            if(disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }






        //用于创建用户信息时上传身份证图片
        public JsonResult Import(HttpPostedFileBase file)
        {
            try
            {
                string path = Server.MapPath("~/");
                string phot_path = FileUpload.SaveFileInUserInfo(file, path, Session["name"].ToString(), Session["id"].ToString());

                return Json(phot_path);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //用于修改身份证图片
        public JsonResult Change_Import(HttpPostedFileBase file,string username)
        {
            try
            {
                string path = Server.MapPath("~/");
                string phot_path = FileUpload.SaveInfo(file, path, username);
                return Json(phot_path);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //用于获取用户信息
        public JsonResult GetMessage()
        {
            return Json(GetUserInfo.Get_UserInfo(Convert.ToInt32(Session["id"])));
        }

        //用于用户编辑信息
        [HttpPost]
        public ActionResult NewEdit(View_UserInfo msg)
        {
            if(ModelState.IsValid)
            {
                string path = Server.MapPath("~/");
                string str = GetUserInfo.Edit_Info(msg, Convert.ToInt32(Session["id"]), path);
                Session["name"] = msg.姓名;
                string newpath = "/UserCardInfo/" + Session["id"].ToString() + "/" + Path.GetFileName(str);
                return Json(newpath);
            }
            return Json(null);
        }

        //检查密码是否正确
        public JsonResult Checkpwd(string pwd)
        {
            try
            {
                return Json(GetUserInfo.Get_pwd(Convert.ToInt32(Session["id"]), pwd));
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        //修改密码
        public JsonResult Edit_pwd(string pwd)
        {
            try
            {
                GetUserInfo.Edit_pwd(Convert.ToInt32(Session["id"]), pwd);
                return Json(true);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
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
