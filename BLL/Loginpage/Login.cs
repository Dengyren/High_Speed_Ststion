using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using BLL.Password;

namespace BLL.Loginpage
{

    public static class Login
    {
        public static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public static TB_pcUser pcUser_Login(string PCid,string password)
        {
            TB_pcUser tb = db.TB_pcUser.Where(c => c.警员编号 == (PCid)).FirstOrDefault();
            if (tb != null)
            {
                if (tb.密码 == password)
                {
                    return tb;
                }
                else
                    return null;
                 
            }
            return null;
        }

        public static TB_user User_Login(string Userid,string pwd)
        {
            try
            {
                TB_user tb = db.TB_user.Where(c => c.身份证号码 == Userid).FirstOrDefault();
                if(tb != null)
                {
                    if(tb.密码 == EncryptUtility.DesEncrypt(pwd))
                    {
                        return tb;
                    }
                    else
                        return null;
                }
                return null;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static bool First_Sign_In(TB_user tB_User)
        {
            if(tB_User.信息编号 == 0)
                return true;
            else
                return false;
        }
        public static TB_StationManager StationManager_Login(string Userid, string pwd)
        {
            try
            {
                TB_StationManager tb = db.TB_StationManager.Where(c => c.身份证号码 == Userid).FirstOrDefault();
                if (tb != null)
                {
                    if (tb.密码 == pwd)
                    {
                        return tb;
                    }
                    else
                        return null;
                }
                return null;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}