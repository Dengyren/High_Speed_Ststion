using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.ShowMTCInfo
{
    public class GetCarPath
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        public List<string> getMsg(int id)
        {
            List<TB_OverSite> list = db.TB_OverSite.Where(c => c.进站编号 == id).ToList();
            List<string> title = new List<string>();
            foreach(var r in list)
            {
                title.Add(Getname(r.站点编号,r.过站时间));
            }
            return title;
        }


        public string Getname(int id,string time)
        {
            string name = null;
            var result = db.TB_Tollgate.Where(c => c.id == id).ToList();
            foreach(var r in result)
            {
                name = r.站点名称 + "(" + time + ")";
            }
            return name;
        }

    }
}