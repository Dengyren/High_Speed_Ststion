using System.Web.Mvc;

namespace 交警信息管理系统.Areas.UserInfoManager
{
    public class UserInfoManagerAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UserInfoManager";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UserInfoManager_default",
                "UserInfoManager/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}