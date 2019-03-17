using System.Web.Mvc;

namespace 后台管理系统.Areas.TollStation
{
    public class TollStationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "TollStation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "TollStation_default",
                "TollStation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}