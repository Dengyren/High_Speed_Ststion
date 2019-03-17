using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace BLL.UserInfoManger.Examine_message
{
    public class Send_Examine
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public void Send(int id)
        {
            try
            {
                User_Examine UserInfo = new User_Examine();
                ExmaineInfoModel msg = UserInfo.Find(id);
                int Aid = AddUserAudit(msg);
                AddCarAudit(msg, Aid);
                EditCarMsg(msg);
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void EditCarMsg(ExmaineInfoModel msg)
        {
            try
            {
                foreach(var r in msg.tb_car)
                {
                    TB_carID tb = db.TB_carID.Find(r.id);
                    tb.状态编号 = 1;
                    db.Entry(tb).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public int AddUserAudit(ExmaineInfoModel msg)
        {
            try
            {
                int id = 0;
                foreach(var r in msg.view_user)
                {
                    var item = db.TB_UserAudit.Where(c => c.用户编号 == r.用户编号 && c.状态编号 == 1);
                    if(item.Count() > 0)
                    {
                        foreach(var i in item)
                        {
                            TB_UserAudit tb = db.TB_UserAudit.Find(i.id);
                            tb.用户编号 = r.用户编号;
                            tb.姓名 = r.姓名;
                            tb.手机号码 = r.手机号码;
                            tb.籍贯 = r.籍贯;
                            tb.身份证号码 = r.身份证号码;
                            tb.驾驶证号码 = r.驾驶证号码;
                            tb.身份证照片 = r.身份证照片;
                            tb.状态编号 = 1;
                            db.Entry(tb).State = EntityState.Modified;
                            id = i.id;
                        }
                        db.SaveChanges();
                        return id;
                    }
                    else
                    {
                        TB_UserAudit tb = new TB_UserAudit
                        {
                            用户编号 = r.用户编号,
                            姓名 = r.姓名,
                            手机号码 = r.手机号码,
                            籍贯 = r.籍贯,
                            身份证号码 = r.身份证号码,
                            驾驶证号码 = r.驾驶证号码,
                            身份证照片 = r.身份证照片,
                            状态编号 = 1,
                        };
                        db.TB_UserAudit.Add(tb);
                        db.SaveChanges();
                        id = tb.id;
                        return id;
                    }

                }
                return id;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void AddCarAudit(ExmaineInfoModel msg, int id)
        {
            try
            {
                foreach(var r in msg.tb_car)
                {
                    if(r.状态编号 == 4)
                    {
                        TB_CarAudit tb = new TB_CarAudit
                        {
                            用户编号 = r.用户编号,
                            车牌号 = r.车牌号,
                            状态编号 = 1,
                            车牌照片前 = r.车牌照片前,
                            车牌照片后 = r.车牌照片后,
                            信息编号 = id

                        };
                        db.TB_CarAudit.Add(tb);
                    }
                }
                db.SaveChanges();
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}