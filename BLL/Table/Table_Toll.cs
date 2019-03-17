using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace BLL.Table
{
    public class Table_Toll
    {
        private  HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        public List<View_TollStation> Get_Toll(string id, int area)
        {
            try
            {
                var takeinArea = db.TB_OwnershipArea.Find(area);
                string AreaName = takeinArea.归属管区;

                List<View_TollStation> result = db.View_TollStation.Where(c => (c.站点名称.Contains(id) && c.归属管区 == AreaName)).ToList();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //public static List<TB_Tollgate> Get_Toll2(string id)
        //{
        //    try
        //    {
        //        var list = new List<TB_Tollgate>();
        //        var result = db.TB_Tollgate.Where(c => c.id.ToString().Contains(id)).ToList();

        //    }
        //    catch (Exception e)
        //    {
        //        throw new Exception(e.Message);
        //    }
        //}

        public  List<View_TollStation> Get_UserToll(string place, string station)
        {
            try
            {
                List<View_TollStation> tb_toll = db.View_TollStation.Where(c => (c.站点名称.Contains(station) && c.归属管区.Contains(place))).ToList();
                return tb_toll;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<TB_OwnershipArea> Get_Owner()
        {
            try
            {
                List<TB_OwnershipArea> tb_OSA = db.TB_OwnershipArea.ToList();
                return tb_OSA;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}