using Microsoft.Owin;
using Owin;

using Microsoft.Owin.Security.DataProtection;


[assembly: OwinStartupAttribute(typeof(SleekSoftMVCFramework.Startup))]
namespace SleekSoftMVCFramework
{

    public partial class Startup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; set; }
        public void Configuration(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();
            ConfigureAuth(app);
            HangfireConfig.ConfigureHangfire(app);
            HangfireConfig.InitializeJobs();

        }
    }



}
