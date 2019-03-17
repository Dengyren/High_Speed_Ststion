using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Table
{
    public class Table_PCuser
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();


        public static List<TB_pcUser> Get_PCuser(string id,string Name)
        {
            try
            {
                var list = new List<TB_pcUser>();
                var result = db.TB_pcUser.Where(c => c.警员编号.Contains(id)&&c.姓名.Contains(Name)).ToList();
                foreach (var item in result)
                {
                    TB_pcUser tB = new TB_pcUser
                    {
                        警员编号=item.警员编号,
                        姓名=item.姓名,
                        性别=item.性别,
                        年龄=item.年龄,
                        归属=item.归属
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