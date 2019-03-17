using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Actomatic_Deduction
{
    public class GetRandomCar
    {

        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public string Getcarid()
        {
            string mycar = null;
            var car = (from a in db.TB_carID.ToList()
                       orderby (Guid.NewGuid())
                       select a).Take(1).ToList();

            foreach(var r in car)
            {
                mycar = r.车牌号;
            }
            return mycar;
        }

        public bool checkCar(string car)
        {
            var result = db.TB_MTC.Where(c => c.车牌号码.Equals(car) && c.状态 == 1).ToList();
            if(result.Count() > 0)
                return true;
            else
                return false;
        }
    }

}