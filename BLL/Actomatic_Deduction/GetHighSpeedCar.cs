using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Actomatic_Deduction
{
    public class GetHighSpeedCar
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public int Get_HighSpeedCar()
        {
            int id = 0;
            var result = (from a in db.TB_MTC.Where(c => c.状态 == 1).ToList()
                          orderby (Guid.NewGuid())
                          select a).Take(1).ToList();
            if(result.Count() > 0)
            {
                foreach(var r in result)
                {
                    id = r.id;
                }
            }
            return id;
        }
    }
}