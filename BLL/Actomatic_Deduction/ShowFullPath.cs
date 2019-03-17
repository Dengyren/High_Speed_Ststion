using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.Actomatic_Deduction
{
    public class ShowFullPath
    {
        private HighSpeedTollSystemEntities db = new HighSpeedTollSystemEntities();

        public Dictionary<string, string> Show(int id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            var result = db.TB_MTC.Find(id);
            dic.Add("CarID", result.车牌号码);
            if(result.中间路径.Equals("无"))
                dic.Add("path", result.进站点 + "-" + result.出站点);
            else
                dic.Add("path", result.进站点 + result.中间路径 + "-" + result.出站点);
            dic.Add("Money", "¥"+result.扣费金额);
            return dic;
        }
    }
}