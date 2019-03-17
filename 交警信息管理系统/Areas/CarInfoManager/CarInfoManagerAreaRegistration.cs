using System.Web.Mvc;

namespace 交警信息管理系统.Areas.CarInfoManager
{
    public class CarInfoManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CarInfoManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CarInfoManager_default",
                "CarInfoManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}