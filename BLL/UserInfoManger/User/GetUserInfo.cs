using BLL.Password;
using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;


namespace BLL.UserInfoManger
{
    public class GetUserInfo
    {

        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public static View_UserInfo Get_UserInfo(int id)
        {
            try
            {
                int num = 0;
                View_UserInfo view_Userinfo = new View_UserInfo();
                var result1 = from r in db.TB_user
                              where r.用户编号 == id
                              select new
                              {
                                  r.用户编号,
                                  r.身份证号码,
                                  r.姓名,
                                  r.信息编号
                              };
                if(result1.Count() > 0)
                {
                    foreach(var r in result1)
                    {
                        view_Userinfo.用户编号 = r.用户编号;
                        view_Userinfo.姓名 = r.姓名;
                        //view_Userinfo.密码 = r.密码;
                        //view_Userinfo.手机号码 = r.手机号码;
                        //view_Userinfo.籍贯 = r.籍贯;
                        view_Userinfo.身份证号码 = r.身份证号码;
                        num = r.信息编号;
                        //view_Userinfo.身份证照片 = "/UserCardInfo/" + id.ToString() + "/" + Path.GetFileName(r.身份证照片);
                        //view_Userinfo.驾驶证号码 = r.驾驶证号码;
                    }
                }
                var result2 = db.TB_UserInfo.Where(c => c.id.Equals(num)).Distinct();
                if(result2.Count() > 0)
                {
                    foreach(var r in result2)
                    {
                        //view_Userinfo.用户编号 = r.用户编号;
                        //view_Userinfo.姓名 = r.姓名;
                        //view_Userinfo.密码 = r.密码;
                        view_Userinfo.手机号码 = r.手机号码;
                        view_Userinfo.籍贯 = r.籍贯;
                        //view_Userinfo.身份证号码 = r.身份证号码;
                        view_Userinfo.身份证照片 = "/UserCardInfo/" + id.ToString() + "/" + Path.GetFileName(r.身份证照片);
                        view_Userinfo.驾驶证号码 = r.驾驶证号码;
                    }
                }
                return view_Userinfo;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static string Edit_Info(View_UserInfo view_UserInfo, int id, string path)
        {
            try
            {
                TB_user tB_user = db.TB_user.Find(id);
                tB_user.姓名 = view_UserInfo.姓名;
                db.Entry(tB_user).State = EntityState.Modified;
                db.SaveChanges();

                TB_UserInfo tB_userInfo = db.TB_UserInfo.Find(tB_user.信息编号);

                string Photo_Path = FileUpload.SaveReadyInfo(view_UserInfo.身份证照片, path, id.ToString());
                if(File.Exists(Photo_Path) && Photo_Path != null) //判断是否存在
                {
                    if(!(tB_userInfo.身份证照片.Equals(Photo_Path)))
                        FileUpload.FileExists(tB_userInfo.身份证照片);
                    tB_userInfo.身份证照片 = Photo_Path;
                }
                if(!(tB_user.姓名.Equals(Path.GetFileNameWithoutExtension(tB_userInfo.身份证照片))))
                    tB_userInfo.身份证照片 = FileUpload.Change_edit_photoName(tB_userInfo.身份证照片, tB_user.姓名);
                tB_userInfo.手机号码 = view_UserInfo.手机号码;
                tB_userInfo.籍贯 = view_UserInfo.籍贯;
                db.Entry(tB_userInfo).State = EntityState.Modified;
                db.SaveChanges();

                return tB_userInfo.身份证照片;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static bool Get_pwd(int id, string pwd)
        {
            try
            {
                TB_user tB_user = db.TB_user.Find(id);
                if(tB_user.密码.Equals(EncryptUtility.DesEncrypt(pwd)))
                    return true;
                else
                    return false;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void Edit_pwd(int id, string pwd)
        {
            try
            {
                TB_user tB_user = db.TB_user.Find(id);
                tB_user.密码 = EncryptUtility.DesEncrypt(pwd);
                db.Entry(tB_user).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}