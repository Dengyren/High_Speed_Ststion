using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.UserInfoManger.Balance
{
    public class User_BalanceInfo
    {

        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        public static int GetBalance(int? id)
        {
            try
            {
                int balance = 0;
                if(id != null && id != 0) 
                {
                    var result = db.TB_user.Find(id);
                    balance = db.TB_UserInfo.Find(result.信息编号).账户余额;

                }
                return balance;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}