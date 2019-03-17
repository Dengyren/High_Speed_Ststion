using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BLL.UserInfoManger
{
    public static class CreateUserInfo
    {
        public static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public static void Create_UserInfo(View_UserInfo view_UserInfo, int id)
        {
            try
            {

                TB_UserInfo tB_Userinfo = new TB_UserInfo()
                {
                    手机号码 = view_UserInfo.手机号码,
                    籍贯 = view_UserInfo.籍贯,
                    账户余额 = 0,
                    身份证照片 = view_UserInfo.身份证照片,
                    驾驶证号码 = view_UserInfo.驾驶证号码,
                };
                db.TB_UserInfo.Add(tB_Userinfo);
                db.SaveChanges();

                TB_user tB_user = db.TB_user.Find(id);
                tB_user.信息编号 = tB_Userinfo.id;
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