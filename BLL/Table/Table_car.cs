using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Table
{
    public static class Table_car
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();


        public static List<View_Car> Get_UsreCar(int id)
        {
            try
            {
                var list = new List<View_Car>();
                var result = db.View_Car.Where(c => c.用户编号 == id);
                if(result.Count() > 0)
                {
                    foreach(var item in result)
                    {
                        if(!(item.车牌照片前.Equals("无")))
                            item.车牌照片前 = "已上传";
                        if(!(item.车牌照片后.Equals("无")))
                            item.车牌照片后 = "已上传";
                        View_Car tB = new View_Car
                        {
                            车牌号 = item.车牌号,
                            车牌照片后 = item.车牌照片后,
                            车牌照片前 = item.车牌照片前,
                            状态 = item.状态,
                            id = item.id,
                        };
                        list.Add(tB);
                    }
                }
                return list;

            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}