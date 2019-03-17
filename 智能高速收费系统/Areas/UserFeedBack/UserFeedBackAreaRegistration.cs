using System.Web.Mvc;

namespace 智能高速收费系统.Areas.UserFeedBack
{
    public class UserFeedBackAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "UserFeedBack";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "UserFeedBack_default",
                "UserFeedBack/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}