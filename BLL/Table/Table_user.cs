using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Table
{
    public class Table_user
    {
        private static HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();


        public static List<View_UserInfo> Get_UsreInfo(string id)
        {
            try
            {
                var list = new List<View_UserInfo>();
                var result = db.View_UserInfo.Where(c=>c.身份证号码.Contains(id)).ToList();
                foreach (var item in result)
                {
                    View_UserInfo tB = new View_UserInfo
                    {
                        用户编号 = item.用户编号,
                        姓名 = item.姓名,
                        身份证号码 = item.身份证号码,
                        手机号码 = item.手机号码,
                        籍贯 = item.籍贯,
                        驾驶证号码 = item.驾驶证号码,
                        身份证照片 = item.身份证照片,
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

        public static List<View_UserInfo> BackGet_UsreInfo(string id,string name)
        {
            try
            {
                var list = new List<View_UserInfo>();
                var result = db.View_UserInfo.Where(c => c.身份证号码.Contains(id)&&c.姓名.Contains(name)).ToList();
                foreach(var item in result)
                {
                    View_UserInfo tB = new View_UserInfo
                    {
                        用户编号 = item.用户编号,
                        姓名 = item.姓名,
                        身份证号码 = item.身份证号码,
                        手机号码 = item.手机号码,
                        籍贯 = item.籍贯,
                        驾驶证号码 = item.驾驶证号码,
                        身份证照片 = item.身份证照片,
                    };
                    list.Add(tB);
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