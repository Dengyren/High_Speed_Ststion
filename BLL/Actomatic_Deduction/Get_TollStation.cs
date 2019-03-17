using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Actomatic_Deduction
{
    public class Get_TollStation
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public TB_Tollgate GetStationMsg()
        {
            TB_Tollgate toll = new TB_Tollgate();
            var station = (from a in db.TB_Tollgate.ToList()
                       orderby (Guid.NewGuid())
                       select a).Take(1).ToList();
            foreach(var r in station)
            {
                toll.id = r.id;
                toll.收费金额 = r.收费金额;
                toll.站点名称 = r.站点名称;
            }
            return toll;
        }

        public bool checkToll(int id,int stationID)
        {
            var result = db.TB_OverSite.Where(c => c.进站编号 == id).ToList();
            foreach(var r in result)
            {
                if(r.站点编号 == stationID)
                    return true;
            }
            return false;
        }
       

    }
}