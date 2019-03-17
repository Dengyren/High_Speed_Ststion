using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Table
{
    public class Table_HighSpeed
    {
        private  HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        public static DateTime time = DateTime.Now;

        public List<TB_MTC> GetInMsg()
        {
            List<TB_MTC> tb_list = db.TB_MTC.Where(c => c.状态 == 1).OrderByDescending(c=>c.id).ToList();
            return tb_list;
        }


        public List<View_StationDetail> GetMidMsg()
        {
            string _time = time.AddMinutes(-5).ToString();
            List<View_StationDetail> tb_list = db.View_StationDetail.Where(c=>(c.过站时间.CompareTo(_time)) > 0).OrderByDescending(c => c.id).ToList();
            return tb_list;
        }


        public List<TB_MTC> GetExitMsg()
        {
            string _time = time.AddMinutes(-5).ToString();
            List<TB_MTC> tb_list = db.TB_MTC.Where(c => c.状态 == 2 && c.出站时间.CompareTo(_time) >0).OrderByDescending(c => c.id).ToList();
            return tb_list;
        }
    }
}