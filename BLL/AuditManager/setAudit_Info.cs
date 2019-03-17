using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BLL.AuditManager
{
    public class setAudit_Info
    {

        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public void SendPass(int id)
        {
            try
            {
                getAudit_Info UserInfo = new getAudit_Info();
                AuditInfo msg = UserInfo.Find(id);
                passUserInfo(msg);
                passCarMsg(msg);
                passCarID(msg);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void SendFail(int id)
        {
            try
            {
                getAudit_Info UserInfo = new getAudit_Info();
                AuditInfo msg = UserInfo.Find(id);
                failUserInfo(msg);
                failCarMsg(msg);
                failCarID(msg);             
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void passCarMsg(AuditInfo msg)
        {
            try
            {
                foreach (var r in msg.tB_CarAudits)
                {
                    TB_CarAudit tb = db.TB_CarAudit.Find(r.id);
                    tb.状态编号 = 3;
                    db.Entry(tb).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        private void failCarMsg(AuditInfo msg)
        {
            try
            {
                foreach (var r in msg.tB_CarAudits)
                {
                    TB_CarAudit tb = db.TB_CarAudit.Find(r.id);
                    tb.状态编号 = 2;
                    db.Entry(tb).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void passUserInfo(AuditInfo msg)
        {
            try
            {
                foreach (var r in msg.tB_UserAudits)
                {

                    TB_UserAudit tb = db.TB_UserAudit.Find(r.id);
                    tb.状态编号 = 3;
                    db.Entry(tb).State = EntityState.Modified;

                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void failUserInfo(AuditInfo msg)
        {
            try
            {
                foreach (var r in msg.tB_UserAudits)
                {

                    TB_UserAudit tb = db.TB_UserAudit.Find(r.id);
                    tb.状态编号 = 2;
                    db.Entry(tb).State = EntityState.Modified;

                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public void passCarID(AuditInfo msg)
        {
            try
            {
                foreach (var r in msg.tB_CarAudits)
                {
                    var item = db.TB_carID.Where(c => c.车牌号 == r.车牌号).ToList();
                    foreach (var i in item)
                    {
                        TB_carID tB_CarID = db.TB_carID.Find(i.id);
                        tB_CarID.状态编号 = 3;
                        db.Entry(tB_CarID).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void failCarID(AuditInfo msg)
        {
            try
            {
                foreach (var r in msg.tB_CarAudits)
                {
                    var item = db.TB_carID.Where(c => c.车牌号 == r.车牌号).ToList();
                    foreach (var i in item)
                    {
                        TB_carID tB_CarID = db.TB_carID.Find(i.id);
                        tB_CarID.状态编号 = 2;
                        db.Entry(tB_CarID).State = EntityState.Modified;
                    }
                }
                db.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}