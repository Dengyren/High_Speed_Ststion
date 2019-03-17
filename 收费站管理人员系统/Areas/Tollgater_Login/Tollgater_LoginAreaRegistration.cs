using System.Web.Mvc;

namespace 收费站管理人员系统.Areas.Tollgater_Login
{
    public class Tollgater_LoginAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Tollgater_Login";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Tollgater_Login_default",
                "Tollgater_Login/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}