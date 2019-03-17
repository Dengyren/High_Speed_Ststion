using System.Web.Mvc;

namespace 交警信息管理系统.Areas.InfoAduit
{
    public class InfoAduitAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "InfoAduit";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "InfoAduit_default",
                "InfoAduit/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}