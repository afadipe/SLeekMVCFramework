using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SleekSoftMVCFramework.Data.Entities;
using SleekSoftMVCFramework.Data.IdentityModel;
using SleekSoftMVCFramework.Repository.CoreRepositories;
using SleekSoftMVCFramework.Repository;
using SleekSoftMVCFramework.Utilities;
using log4net;

namespace SleekSoftMVCFramework.Controllers
{
    public class FrameworkConfigController : Controller
    {
        private readonly IRepositoryQuery<Application, int> _applicationQuery;
        private readonly IRepositoryQuery<ApplicationUser, long> _applicationUserQuery;
        private readonly IActivityLogRepositoryCommand _activityRepo;
        private readonly ILog _log;
        public FrameworkConfigController(IActivityLogRepositoryCommand activityRepo, IRepositoryQuery<Application, int> application, IRepositoryQuery<ApplicationUser,long> applicationUser,ILog log)
        {
            _applicationQuery = application;
            _applicationUserQuery = applicationUser;
            _activityRepo = activityRepo;
            _log = log;
        }
        // GET: FrameworkConfig
        [AllowAnonymous]
        public ActionResult Index()
        {
            try
            {
                _log.InfoFormat("SleekSoft MVC Framework Config checked @ : {0}", DateTime.Now);
                _activityRepo.CreateActivityLog("In Framework setting checking if application portal has being configured", this.GetContollerName(), this.GetContollerName(), 0, null);
                if (_applicationQuery.Count() >= 1)
                {
                    if (_applicationQuery.GetAll().FirstOrDefault().HasAdminUserConfigured)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    else
                    {
                        return RedirectToAction("Start", "FrameworkSetup", new { area = "Portal" });
                    }

                }
                else
                {
                    return RedirectToAction("Start", "FrameworkSetup", new { area = "Portal" });
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return View("Error");
            }
        }
    }
}