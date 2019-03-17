using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.UserPage
{
    public class UserHomePage
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public Dictionary<string , int> GetUserMsg(int id)
        {
            try
            {
                Dictionary<string, int> HPM = new Dictionary<string, int>();
                int money = GetUserBalance(id);
                HPM.Add("3",money);
                int recharge = GetRecharge(id);
                HPM.Add("1", recharge);
                int deduction = GetDeduction(id);
                HPM.Add("2", deduction);
                HPM = Get_Car(HPM, id);
                return HPM;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }

        }

        public int GetUserBalance(int id)
        {
            int Userid = db.TB_user.Find(id).信息编号;
            int money = db.TB_UserInfo.Find(Userid).账户余额;
            return money;
        }

        public int GetRecharge(int id)
        {

            int money = 0;
            var result = db.TB_Balance.Where(c => c.操作id == 1&&c.用户编号==id);
            foreach(var r in result)
            {
                money += r.操作金额;
            }
            return money;
        }

        public int GetDeduction(int id)
        {

            int money = 0;
            var result = db.TB_Balance.Where(c => c.操作id == 2 && c.用户编号 == id);
            foreach(var r in result)
            {
                money += r.操作金额;
            }
            return money;
        }

        public Dictionary<string, int> Get_Car(Dictionary<string, int> dic,int id)
        {
            var result = db.TB_carID.Where(c => c.用户编号 == id).ToList();
            foreach(var r in result)
            {
                var item = db.TB_MTC.Where(c => c.车牌号码.Equals(r.车牌号)).ToList();
                int money = 0;
                foreach(var i in item)
                {
                    money +=Convert.ToInt32(i.扣费金额);
                }
                dic.Add(r.车牌号,money);
            }
            return dic;
        }
    }
}