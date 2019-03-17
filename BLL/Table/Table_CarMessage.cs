using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Table
{
    public class Table_CarMessage
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();


        public static List<View_CarInfo> Get_CarInfo(string id,string CarId,string Name)
        {
            try
            {
                var list = new List<View_CarInfo>();
                var result = db.View_CarInfo.Where(c => c.身份证号码.Contains(id)&&c.车牌号.Contains(CarId)&&c.姓名.Contains(Name)).ToList();
                foreach (var item in result)
                {
                    View_CarInfo tB = new View_CarInfo
                    {
                        id =item.id,
                        姓名=item.姓名,
                        用户编号=item.用户编号,
                        身份证号码=item.身份证号码,
                        状态编号=item.状态编号,
                        车牌号=item.车牌号,
                        车牌照片前=item.车牌照片前,
                        车牌照片后=item.车牌照片后
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