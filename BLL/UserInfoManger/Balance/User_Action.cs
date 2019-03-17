using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BLL.UserInfoManger.Balance
{
    public class User_Action
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public static void User_Recharge(int money,int id)
        {
            try
            {
                TB_UserInfo result = db.TB_UserInfo.Find(db.TB_user.Find(id).信息编号);
                TB_Balance tb_balance = new TB_Balance
                {
                    当时余额 = result.账户余额,
                    操作id = 1,
                    操作金额 = money,
                    时间 = DateTime.Now.ToString(),
                    用户编号 = id
                };
                db.TB_Balance.Add(tb_balance);
                db.SaveChanges();

                result.账户余额 += money;
                db.Entry(result).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


    }
}