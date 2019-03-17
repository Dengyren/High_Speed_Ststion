using BLL.Password;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Loginpage
{
   
    public static class SignUp
    {
        public static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public static  bool UserSignup(string username,string pwd,string IDCard)
        {
            TB_user user = new TB_user();

            try
            {
                var result = db.TB_user.Where(c => c.身份证号码.Equals(IDCard));

                if(result.Count() == 0)
                {
                    user.姓名 = username;
                    user.密码 = EncryptUtility.DesEncrypt(pwd);
                    user.身份证号码 = IDCard;
                    user.信息编号 = 0;
                    db.TB_user.Add(user);
                    db.SaveChanges();
                }
                return true;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}