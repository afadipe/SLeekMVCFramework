using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace SleekSoftMVCFramework
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private ILog _log;
        protected void Application_Start()
        {
            XmlConfigurator.Configure();
            _log = log4net.LogManager.GetLogger("");
            _log.InfoFormat(" <<<< Staring ::: SleekSoft Framework checking Startup Config Setting @ : {0} >>>>>>", DateTime.Now);

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutofacConfig.ConfigureContainer();
            _log.InfoFormat(" <<<< Ending ::: SleekSoft Framework Startup Config Setting ok @ : {0} >>>>>>", DateTime.Now);
        }
    }
}
