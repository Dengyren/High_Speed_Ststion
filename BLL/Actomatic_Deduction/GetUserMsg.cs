using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Actomatic_Deduction
{
    public class GetUserMsg
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        public int GetUserID(string carname)
        {
            int id = 0;
            var result = db.TB_carID.Where(c => c.车牌号.Equals(carname)).ToList();
            foreach(var r in result)
            {
                id = r.用户编号;
            }
            return id;
        }
    }
}