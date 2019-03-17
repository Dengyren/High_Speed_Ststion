using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BLL.Actomatic_Deduction
{
    public class HighSpeedCarManger
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();


        public void CreateCarMsg(string carname)
        {
            try
            {
                GetUserMsg user = new GetUserMsg();
                int Userid = user.GetUserID(carname);
                Get_TollStation station = new Get_TollStation();
                TB_Tollgate toll = station.GetStationMsg();
                TB_MTC tb = new TB_MTC()
                {
                    车牌号码 = carname,
                    进站时间 = DateTime.Now.ToString(),
                    状态 = 1,
                    进站点 = toll.站点名称,
                    扣费金额 = toll.收费金额,
                    中间路径 = "无"
                };
                db.TB_MTC.Add(tb);
                db.SaveChanges();

                TB_user tb_user = db.TB_user.Find(Userid);
                TB_UserInfo tb_info = db.TB_UserInfo.Find(tb_user.信息编号);

                TB_Balance tb_balance = new TB_Balance
                {
                    当时余额 = tb_info.账户余额,
                    操作id = 2,
                    操作金额 = toll.收费金额,
                    时间 = DateTime.Now.ToString(),
                    用户编号 = Userid
                };
                db.TB_Balance.Add(tb_balance);
                db.SaveChanges();

                tb_info.账户余额 -= toll.收费金额;
                db.Entry(tb_info).State = EntityState.Modified;
                db.SaveChanges();


                TB_OverSite tb_over = new TB_OverSite
                {
                    站点编号= toll.id,
                    过站时间 = tb.进站时间,
                    进站编号= tb.id
                };
                db.TB_OverSite.Add(tb_over);
                db.SaveChanges();

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void AddStationMsg(int id)
        {
            GetUserMsg user = new GetUserMsg();
            Get_TollStation station = new Get_TollStation();
            TB_Tollgate toll=station.GetStationMsg();
            while(station.checkToll(id, toll.id))
            {
                toll = station.GetStationMsg();
            }


            TB_MTC tb = db.TB_MTC.Find(id);
            int Userid = user.GetUserID(tb.车牌号码);
            if(tb.中间路径.Equals("无"))
                tb.中间路径 = "-" + toll.站点名称;
            else
                tb.中间路径 += "-" + toll.站点名称;
            tb.扣费金额 += toll.收费金额;
            db.Entry(tb).State = EntityState.Modified;
            db.SaveChanges();

            TB_user tb_user = db.TB_user.Find(Userid);
            TB_UserInfo tb_info = db.TB_UserInfo.Find(tb_user.信息编号);

            TB_Balance tb_balance = new TB_Balance
            {
                当时余额 = tb_info.账户余额,
                操作id = 2,
                操作金额 = toll.收费金额,
                时间 = DateTime.Now.ToString(),
                用户编号 = Userid
            };
            db.TB_Balance.Add(tb_balance);
            db.SaveChanges();

            tb_info.账户余额 -= toll.收费金额;
            db.Entry(tb_info).State = EntityState.Modified;
            db.SaveChanges();

            TB_OverSite tb_over = new TB_OverSite
            {
                站点编号 = toll.id,
                过站时间 = tb.进站时间,
                进站编号 = tb.id
            };
            db.TB_OverSite.Add(tb_over);
            db.SaveChanges();
        }


        public void ExitStationMsg(int id)
        {
            GetUserMsg user = new GetUserMsg();
            Get_TollStation station = new Get_TollStation();
            TB_Tollgate toll = station.GetStationMsg();
            while(station.checkToll(id, toll.id))
            {
                toll = station.GetStationMsg();
            }


            TB_MTC tb = db.TB_MTC.Find(id);
            int Userid = user.GetUserID(tb.车牌号码);

            tb.出站点 = toll.站点名称;
            tb.出站时间 = DateTime.Now.ToString();
            tb.状态 = 2;
            tb.扣费金额 += toll.收费金额;
            db.Entry(tb).State = EntityState.Modified;
            db.SaveChanges();

            TB_user tb_user = db.TB_user.Find(Userid);
            TB_UserInfo tb_info = db.TB_UserInfo.Find(tb_user.信息编号);

            TB_Balance tb_balance = new TB_Balance
            {
                当时余额 = tb_info.账户余额,
                操作id = 2,
                操作金额 = toll.收费金额,
                时间 = DateTime.Now.ToString(),
                用户编号 = Userid
            };
            db.TB_Balance.Add(tb_balance);
            db.SaveChanges();

            tb_info.账户余额 -= toll.收费金额;
            db.Entry(tb_info).State = EntityState.Modified;
            db.SaveChanges();

            TB_OverSite tb_over = new TB_OverSite
            {
                站点编号 = toll.id,
                过站时间 = tb.进站时间,
                进站编号 = tb.id
            };
            db.TB_OverSite.Add(tb_over);
            db.SaveChanges();
        }


    }
}