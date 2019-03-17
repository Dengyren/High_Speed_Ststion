using System.Web.Mvc;

namespace 收费站管理人员系统.Areas.StationDetail
{
    public class StationDetailAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "StationDetail";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "StationDetail_default",
                "StationDetail/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}