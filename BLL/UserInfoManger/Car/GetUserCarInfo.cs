using DAL;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace BLL.UserInfoManger
{
    public static class GetUserCarInfo
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();




        public static List<string> GetDelete_CarInfo(int id,string Userid)
        {
            try
            {
                TB_carID tB_Car = db.TB_carID.Find(id);
                List<string> list = new List<string>();
                tB_Car.车牌照片前= "/UserCardInfo/" + Userid + "/" + Path.GetFileName(tB_Car.车牌照片前);
                list.Add(tB_Car.车牌照片前);
                tB_Car.车牌照片后 = "/UserCardInfo/" + Userid + "/" + Path.GetFileName(tB_Car.车牌照片后);
                list.Add(tB_Car.车牌照片后);
                return list;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public static Dictionary<string, string> Get_UsreCarid(int id)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                var result = db.TB_carID.Where(c => c.用户编号 == id);
                if(result.Count() > 0)
                {
                    foreach(var r in result)
                    {
                        dic.Add(r.id.ToString(), r.车牌号);
                    }
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