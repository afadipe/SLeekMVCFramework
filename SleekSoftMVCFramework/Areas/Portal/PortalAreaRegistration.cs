using System.Web.Mvc;

namespace SleekSoftMVCFramework.Areas.Portal
{
    public class PortalAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Portal";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Portal_default",
                "Portal/{controller}/{action}/{id}",
                new { controller = "FrameworkSetup", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}