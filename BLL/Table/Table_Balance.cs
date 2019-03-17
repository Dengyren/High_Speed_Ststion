using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Table
{
    public class Table_Balance
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();
        public static List<View_Action> Get_UsreBalance(int id, int? action)
        {
            try
            {
                List<View_Action> list = new List<View_Action>();
                if(action != null)
                    list = db.View_Action.Where(c => c.操作id == action && c.用户编号 == id).OrderByDescending(c => c.id).ToList();
                else
                    list = db.View_Action.Where(c => c.用户编号 == id).OrderByDescending(c=>c.id).ToList();
                return list;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Dictionary<string, string> Get_Actionid()
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                var result = db.TB_Action.Select(c => c);
                if(result.Count() > 0)
                {
                    foreach(var r in result)
                    {
                        dic.Add(r.id.ToString(), r.操作动作);
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