using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace BLL.AuditManager
{
    public class getAudit_Info
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public Dictionary<string, Dictionary<string, string>> FindMsg(int id)
        {
            try
            {
                Dictionary<string, Dictionary<string, string>> dic = new Dictionary<string, Dictionary<string, string>>();
                Dictionary<string, string> car = Get_Car(id);
                Dictionary<string, string> user = Get_User(id);
                dic.Add("UserInfo", user);
                dic.Add("CarInfo", car);
                return dic;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Dictionary<string, string> Get_Car(int id)
        {
            try
            {
                var tb = db.TB_CarAudit.Where(c => c.用户编号 == id).ToList();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var r in tb)
                {
                    dic.Add(r.车牌号, r.车牌号);
                    dic.Add(r.车牌照片前, " /UserCardInfo/" + r.用户编号.ToString() + "/" + Path.GetFileName(r.车牌照片前));
                    dic.Add(r.车牌照片后, " /UserCardInfo/" + r.用户编号.ToString() + "/" + Path.GetFileName(r.车牌照片后));
                }
                return dic;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Dictionary<string, string> Get_User(int id)
        {
            try
            {
                var tb = db.TB_UserAudit.Where(c => c.用户编号 == id).Distinct().ToList();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                foreach (var r in tb)
                {
                    dic.Add("姓名", r.姓名);
                    dic.Add("手机号码", r.手机号码);
                    dic.Add("籍贯", r.籍贯);
                    dic.Add("身份证号码", r.身份证号码);
                    dic.Add("身份证照片", "/UserCardInfo/" + r.用户编号.ToString() + "/" + Path.GetFileName(r.身份证照片));
                    dic.Add("驾驶证号码", r.驾驶证号码);
                }
                return dic;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public AuditInfo Find(int id)
        {
            AuditInfo UserInfo = new AuditInfo
            {
                tB_UserAudits = db.TB_UserAudit.Where(c => c.用户编号 == id).ToList(),
                
                tB_CarAudits = db.TB_CarAudit.Where(c => c.用户编号 == id && c.状态编号 == 1).ToList()
            };
            //UserInfo = change_tb_car(UserInfo, id);
            //UserInfo = change_view_user(UserInfo, id);
            return UserInfo;
        }

        public AuditInfo change_tb_car(AuditInfo UserInfo, int id)
        {
            foreach (var r in UserInfo.tB_CarAudits)
            {
                r.车牌照片前 = "/UserCardInfo/" + id.ToString() + "/" + Path.GetFileName(r.车牌照片前);
                r.车牌照片后 = "/UserCardInfo/" + id.ToString() + "/" + Path.GetFileName(r.车牌照片后);
            }
            return UserInfo;
        }

        public AuditInfo change_view_user(AuditInfo UserInfo, int id)
        {
            foreach (var r in UserInfo.tB_UserAudits)
            {
                r.身份证照片 = "/UserCardInfo/" + id.ToString() + "/" + Path.GetFileName(r.身份证照片);
            }
            return UserInfo;
        }

        public int GetAuditCount()
        {
            int count=0;
            AuditInfo UserInfo = new AuditInfo
            {
                tB_UserAudits = db.TB_UserAudit.Where(c => c.状态编号 == 1).ToList(),
            };
             foreach(var r in UserInfo.tB_UserAudits)
            {
                count++;
            }
            return count;
        }

    }
}