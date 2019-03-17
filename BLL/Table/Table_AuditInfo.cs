using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Table
{
    public class Table_AuditInfo
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();


        public static List<TB_UserAudit> Get_UserInfo()
        {
            try
            {
                var list = new List<TB_UserAudit>();
                var result = db.TB_UserAudit.ToList();
                foreach (var item in result)
                {
                    TB_UserAudit tB = new TB_UserAudit
                    {
                        id = item.id,
                        姓名 = item.姓名,
                        身份证号码 = item.身份证号码,
                        用户编号 = item.用户编号,
                        状态编号 = item.状态编号
                    };
                    list.Add(tB);
                }
                return list;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}