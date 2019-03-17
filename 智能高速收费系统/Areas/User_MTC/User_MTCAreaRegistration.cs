using System.Web.Mvc;

namespace 智能高速收费系统.Areas.User_MTC
{
    public class User_MTCAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "User_MTC";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "User_MTC_default",
                "User_MTC/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}