using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Table
{
    public static class Table_MTC
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        public static List<TB_MTC> Get_UsreMTC(int id, string carid,string tollName)
        {
            try
            {
                var list = new List<TB_MTC>();
                var result = from r in db.TB_carID
                             where (r.用户编号 == id && r.车牌号.Contains(carid))
                             select new
                             {
                                 r.车牌号
                             };
                foreach(var r in result)
                {
                    var item = db.TB_MTC.Where(c => (c.车牌号码.Equals(r.车牌号)) && (c.出站点.Contains(tollName) || c.进站点.Contains(tollName))).ToList();
                    foreach(var n in item)
                    {
                        list.Add(n);
                    }
                }

                return list;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Dictionary<string, string> Get_Car(int id)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                var tb = db.TB_carID.Where(c => c.用户编号 == id);
                foreach(var r in tb)
                {
                    dic.Add(r.车牌号, r.车牌号);
                }
                return dic;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}