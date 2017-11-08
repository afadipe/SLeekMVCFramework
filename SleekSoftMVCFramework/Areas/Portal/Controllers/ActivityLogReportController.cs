using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using SleekSoftMVCFramework.Data.IdentityModel;
using SleekSoftMVCFramework.Data.Entities;
using SleekSoftMVCFramework.Repository.CoreRepositories;
using SleekSoftMVCFramework.ViewModel;
using System.Data.SqlClient;
using log4net;
using System.Reflection;
using SleekSoftMVCFramework.Data.Core;
using System.Text.RegularExpressions;

namespace SleekSoftMVCFramework.Areas.Portal.Controllers
{
    public class ActivityLogReportController : Controller
    {

        private readonly IRepositoryQuery<ActivityLog,long> _activitylogQuery;
        private readonly IRepositoryCommand<ActivityLog, long> _activitylogCommand;
        private readonly IRepositoryQuery<ApplicationUser, long> _applicationUserQuery;
        private readonly ILog _log;

        public ActivityLogReportController(IRepositoryCommand<ActivityLog, long> activitylogCommand, IRepositoryQuery<ActivityLog, long> activitylogQuery, IRepositoryQuery<ApplicationUser, long> applicationUserQuery, ILog log)
        {
            _activitylogCommand = activitylogCommand;
            _activitylogQuery = activitylogQuery;
            _applicationUserQuery = applicationUserQuery;
            _log = log;
        }

        // GET: Portal/ActivityLogReport
        public async Task<ActionResult> ActivityLog()
        {
            try
            {
               
                LoadViewDataForDropDownList();
                var activitylogModel = await _activitylogQuery.StoreprocedureQuery<ActivityInfo>("SpGetAllActivityLog").ToListAsync();
                ViewData["SearchResult"] = activitylogModel;
                return View();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<ActionResult> ActivityLog(ActivitlogSearchInfo searchvm)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchvm.SelectedController))
                {
                    int Textlength = searchvm.SelectedController.Length;
                    int removelength = "Controller".ToString().Length;

                    string realText = searchvm.SelectedController.Substring(Textlength, removelength);
                   // string realText = Regex.Replace(searchvm.SelectedController, @"\([Controller]\)", "");
                    searchvm.SelectedController = realText;
                }
                var activitylogModel = await _activitylogQuery.StoreprocedureQueryFor<ActivityInfo>("SpGetActivitlog  @UserId,@Controller,@StartDate,@EndDate", new SqlParameter("UserId", searchvm.SelectedUser),new SqlParameter("controller", searchvm.SelectedController), new SqlParameter("StartDate", searchvm.SelectedStartDate), new SqlParameter("EndDate", searchvm.SelectedEndDate)).ToListAsync();
                ViewData["SearchResult"] = activitylogModel;
                LoadViewDataForDropDownList();
                return View("");
            }
            catch (Exception ex)
            {

                _log.Error(ex);
                return View("Error");
            }
        }


        private static List<Type> GetSubClasses<T>()
        {
            return Assembly.GetCallingAssembly().GetTypes().Where(type => type.IsSubclassOf(typeof(T))).ToList();
        }
        
        public IEnumerable<SelectListItem> GetControllerNames()
        {
            var types = GetSubClasses<Controller>().Select(x => new SelectListItem
            {
                Value = x.Name,
                Text = x.Name
            }).AsEnumerable();
            return new SelectList(types, "Value", "Text");
        }
        

        private void LoadViewDataForDropDownList()
        {
            try
            {
                ViewData["ControllerList"] = GetControllerNames();
                ViewData["UserList"] = _applicationUserQuery.GetAll().Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = x.FirstName + " " + x.MiddleName + " " + x.LastName
                }).AsEnumerable();
            }
            catch (Exception ex)
            {
                _log.Error(ex);

            }

        }

    }
}