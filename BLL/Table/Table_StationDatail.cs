using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;

namespace BLL.Table
{
    public class Table_StationDatail
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        public  List<View_StationDetail> Get_Toll(int id)
        {
            try
            {
                //var list = new List<TB_Tollgate>();
                //var result = db.TB_Tollgate.Where(c => (c.站点名称.Contains(id) && c.归属管区 == (string)area)).ToList();
                //var list = new List<View_StationDetail>();

                //var result= from  t in db.TB_Tollgate where t.id ==id
                //            join  o in db.TB_OverSite  on t.id equals o.站点编号 
                //            join m in db.TB_MTC on o.进站编号 equals m.id
                //            select new {
                //                t.站点名称,
                //                t.收费金额,
                //                o.过站时间,
                //                m.车牌号码
                //            };

                var Station_id = db.TB_Tollgate.Find(id).id;
                List<View_StationDetail> result = db.View_StationDetail.Where(c => c.id == Station_id).ToList();
                return (List <View_StationDetail> )result;

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}