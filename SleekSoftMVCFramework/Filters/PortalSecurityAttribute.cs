using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace VATMVCAPPFramework.Filters
{
    public class PortalSecurityAttribute
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public sealed class ZAPPSecurityAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                filterContext.Controller.ViewBag.UpdateSucceed = "0";
                if (!filterContext.Controller.ViewData.ModelState.IsValid)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }

                var modelList = filterContext.ActionParameters.Where(ap => ap.Key == "model").ToList();
                if (modelList.IsNullOrEmpty())
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }
                if (!modelList.Any() || modelList.Count != 1)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }

                var model = modelList[0].Value as UserPasswordContract;

                if (model == null)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }
                if (
                  string.Compare(model.OldPassword.Trim(), model.NewPassword.Trim(),
                      StringComparison.InvariantCultureIgnoreCase) == 0)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Old Password and New Password and must be different");
                    return;
                }

                if (
                    string.Compare(model.ConfirmPassword.Trim(), model.NewPassword.Trim(),
                        StringComparison.InvariantCultureIgnoreCase) != 0)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "New Password and Confirm New Password must match");
                    return;
                }

                string msg;
                if (!UserAuthentication.ChangeFirstLoginPassword(model.UserName, model.OldPassword, model.NewPassword, out msg))
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", msg.Length > 0 ? msg : "Process Failed! Unable to update password");
                    return;
                }

                var myProfile = MvcApplication.MyUserProfile(model.UserName);
                if (myProfile != null)
                {
                    myProfile.UserFirstLogin = 0;
                    MvcApplication.SetUserProfile(model.UserName, myProfile);
                }
                filterContext.Controller.ViewBag.UpdateSucceed = "1";
                base.OnActionExecuting(filterContext);
            }
        }
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public sealed class ZAPPModifySecurityAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                filterContext.Controller.ViewBag.UpdateSucceed = "0";
                if (!filterContext.Controller.ViewData.ModelState.IsValid)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }

                var modelList = filterContext.ActionParameters.Where(ap => ap.Key == "model").ToList();
                if (modelList.IsNullOrEmpty())
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }
                if (!modelList.Any() || modelList.Count != 1)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }

                var model = modelList[0].Value as UserPasswordContract;

                if (model == null)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }
                string msg;
                if (!UserAuthentication.ChangePassword(model.UserName, model.OldPassword, model.NewPassword, out msg))
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", msg.Length > 0 ? msg : "Process Failed! Unable to update password");
                    return;
                }

                filterContext.Controller.ViewBag.UpdateSucceed = "1";
                base.OnActionExecuting(filterContext);
            }
        }
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public sealed class ZAPPAdminModifySecurityAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                filterContext.Controller.ViewBag.UpdateSucceed = "0";
                if (!filterContext.Controller.ViewData.ModelState.IsValid)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }

                var modelList = filterContext.ActionParameters.Where(ap => ap.Key == "model").ToList();
                if (modelList.IsNullOrEmpty())
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }
                if (!modelList.Any() || modelList.Count != 1)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }

                var model = modelList[0].Value as UserPasswordResetContract;

                if (model == null)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid update information");
                    return;
                }
                string msg;
                if (!UserAuthentication.ResetPassword(model.UserName, out msg))
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", msg.Length > 0 ? msg : "Process Failed! Unable to update password");
                    return;
                }

                filterContext.Controller.ViewBag.UpdateSucceed = "1";
                filterContext.Controller.ViewBag.ThisNewPassword = msg;
                base.OnActionExecuting(filterContext);
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public sealed class ZAPPUnlockSecurityAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                filterContext.Controller.ViewBag.UpdateSucceed = "0";
                if (!filterContext.Controller.ViewData.ModelState.IsValid)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid user information");
                    return;
                }

                var modelList = filterContext.ActionParameters.Where(ap => ap.Key == "model").ToList();
                if (modelList.IsNullOrEmpty())
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid user information");
                    return;
                }
                if (!modelList.Any() || modelList.Count != 1)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid user information");
                    return;
                }

                var model = modelList[0].Value as UserPasswordResetContract;

                if (model == null)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid user information");
                    return;
                }
                string msg;
                if (!UserAuthentication.UnlockUser(model.UserName, out msg))
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", msg.Length > 0 ? msg : "Process Failed! Unable to update password");
                    return;
                }

                filterContext.Controller.ViewBag.UpdateSucceed = "1";
                base.OnActionExecuting(filterContext);
            }
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public sealed class ZAPPPortalAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var model = new MyPersonalizedModel();

                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    filterContext.Controller.ViewBag.UserAuthInfo = null;
                    base.OnActionExecuting(filterContext);
                    return;
                }

                model.MyUserName = filterContext.HttpContext.User.Identity.Name;
                var frmId = (FormsIdentity)filterContext.HttpContext.User.Identity;
                var usData = frmId.Ticket.UserData;
                if (string.IsNullOrEmpty(usData))
                {
                    filterContext.Controller.ViewBag.UserAuthInfo = null;
                    base.OnActionExecuting(filterContext);
                    return;
                }

                model.MyId = int.Parse(usData);

                //Load user detail
                var userProfile = MvcApplication.MyUserProfile(model.MyUserName);
                if (userProfile == null)
                {
                    filterContext.Controller.ViewBag.UserAuthInfo = null;
                    base.OnActionExecuting(filterContext);
                    return;
                }
                userProfile.Roles = string.Join(";", Roles.GetRolesForUser(model.MyUserName));

                var portalProfile = MvcApplication.MyPortalProfile(model.MyUserName);
                if (portalProfile == null)
                {
                    filterContext.Controller.ViewBag.UserAuthInfo = null;
                    base.OnActionExecuting(filterContext);
                    return;
                }

                var tabs = MvcApplication.MyTabSetting(model.MyUserName);
                if (tabs == null)
                {
                    filterContext.Controller.ViewBag.UserAuthInfo = null;
                    base.OnActionExecuting(filterContext);
                    return;
                }

                model.MyMenuList = tabs;
                model.MyPortalSetting = portalProfile;
                model.MyUserProfile = userProfile;

                filterContext.Controller.ViewBag.UserAuthInfo = model;
                base.OnActionExecuting(filterContext);
            }

        }
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public sealed class ZAPPPortalTabAttribute : ActionFilterAttribute
        {

            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var model = filterContext.Controller.ViewBag.UserAuthInfo as MyPersonalizedModel;

                filterContext.Controller.ViewBag.LeftPanelActionName = "";
                filterContext.Controller.ViewBag.LeftPanelControllerName = "";
                filterContext.Controller.ViewBag.LeftPanelRouteParam = "";

                filterContext.Controller.ViewBag.ContentPanelActionName = "";
                filterContext.Controller.ViewBag.ContentPanelControllerName = "";
                filterContext.Controller.ViewBag.ContentPanelRouteParam = "";

                filterContext.Controller.ViewBag.RightPanelActionName = "";
                filterContext.Controller.ViewBag.RightPanelControllerName = "";
                filterContext.Controller.ViewBag.RightPanelRouteParam = "";

                if (model == null)
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }

                if (model.MyMenuList.Count < 1)
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }

                //Get the Action Arguments
                var queryString = filterContext.HttpContext.Request.QueryString;

                var thisTabId = string.IsNullOrEmpty(queryString["tabId"]) ? 0 : int.Parse(queryString["tabId"]);
                var thisTabParentId = string.IsNullOrEmpty(queryString["tabParentId"]) ? 0 : int.Parse(queryString["tabParentId"]);
                var thisTabType = string.IsNullOrEmpty(queryString["tabtype"]) ? 0 : int.Parse(queryString["tabtype"]);
                var thisRndChecker = string.IsNullOrEmpty(queryString["rndChecker"]) ? 0 : int.Parse(queryString["rndChecker"]);


                var portalNavType = (PortalMenuType)Enum.Parse(typeof(PortalMenuType), model.MyPortalSetting.PortalMenuType.ToString(CultureInfo.InvariantCulture));

                switch (portalNavType)
                {
                    case PortalMenuType.HorizontalDropMenu:
                        //filterContext.Controller.ViewBag.DefaultStylePath = "~/Content/zapp/horizontaldropmenu/css";
                        filterContext.Controller.ViewBag.MenuType = 1;
                        break;
                    case PortalMenuType.HorizontalTabMenu:
                        //filterContext.Controller.ViewBag.DefaultStylePath = "~/Content/zapp/horizontaltabmenu/css";
                        filterContext.Controller.ViewBag.MenuType = 2;
                        break;
                    case PortalMenuType.VerticalTabMenu:
                        //filterContext.Controller.ViewBag.DefaultStylePath = "~/Content/zapp/verticaltabmenu/css";
                        filterContext.Controller.ViewBag.MenuType = 3;
                        break;
                    default:
                        //filterContext.Controller.ViewBag.DefaultStylePath = "~/Content/zapp/horizontaldropmenu/css";
                        filterContext.Controller.ViewBag.MenuType = 1;
                        break;
                }

                if (!LoadTabs(model.MyMenuList, thisTabId, thisTabParentId, thisTabType, filterContext))
                {
                    base.OnActionExecuting(filterContext);
                    return;
                }


                filterContext.Controller.ViewBag.CurrentClient = "Test";
                filterContext.Controller.ViewBag.UserIconSource = GetImageSrcName(model.MyUserProfile, model.MyPortalSetting.PictureBesideUsername > 0, filterContext);
                filterContext.Controller.ViewBag.UserName = model.MyUserName;



                if (thisRndChecker > 0)
                {
                    switch (thisRndChecker)
                    {
                        case 1: //My Profile
                            filterContext.Controller.ViewBag.ContentPanelActionName = "MyProfile";
                            filterContext.Controller.ViewBag.ContentPanelControllerName = "PortalProfile";
                            filterContext.Controller.ViewBag.ContentPanelRouteParam = "Portal";
                            break;
                        case 2:
                            filterContext.Controller.ViewBag.ContentPanelActionName = "ChangeMyPassword";
                            filterContext.Controller.ViewBag.ContentPanelControllerName = "PortalProfile";
                            filterContext.Controller.ViewBag.ContentPanelRouteParam = "Portal";
                            break;
                        case 3:
                            filterContext.Controller.ViewBag.ContentPanelActionName = "MySecreteQuestions";
                            filterContext.Controller.ViewBag.ContentPanelControllerName = "PortalProfile";
                            filterContext.Controller.ViewBag.ContentPanelRouteParam = "Portal";
                            break;
                        case 4:
                            filterContext.Controller.ViewBag.ContentPanelActionName = "ChangeMyPassword";
                            filterContext.Controller.ViewBag.ContentPanelControllerName = "PortalProfile";
                            filterContext.Controller.ViewBag.ContentPanelRouteParam = "Portal";
                            break;
                    }
                }
                else
                {
                    if (thisTabId < 2) //Because of root tab
                    {
                        base.OnActionExecuting(filterContext);
                        return;
                    }

                    var curTab = GetCurrentTab(thisTabId, thisTabParentId, model.MyMenuList);
                    if (curTab == null)
                    {
                        base.OnActionExecuting(filterContext);
                        return;
                    }
                    if (curTab.TabParentId == 1)
                    {
                        filterContext.Controller.ViewBag.ContentPanelActionName = "Index";
                        filterContext.Controller.ViewBag.ContentPanelControllerName = "DefaultPage";
                        filterContext.Controller.ViewBag.ContentPanelRouteParam = "Portal";

                    }
                    else
                    {
                        filterContext.Controller.ViewBag.CurrentlySelectedTab = curTab;

                        if (curTab.ContentActionName.Length < 1)
                        {
                            base.OnActionExecuting(filterContext);
                            return;
                        }

                        filterContext.Controller.ViewBag.ContentPanelActionName = curTab.ContentActionName;
                        filterContext.Controller.ViewBag.ContentPanelControllerName = curTab.ContentControllerName;
                        if (curTab.ContentAreaName.Length > 0)
                        {
                            filterContext.Controller.ViewBag.ContentPanelRouteParam = curTab.ContentAreaName;
                        }


                        if (!string.IsNullOrEmpty(curTab.LeftPanelActionName))
                        {
                            filterContext.Controller.ViewBag.LeftPanelActionName = curTab.LeftPanelActionName;
                            filterContext.Controller.ViewBag.LeftPanelControllerName = curTab.LeftPanelControllerName;
                            if (curTab.LeftPanelAreaName.Length > 0)
                            {
                                filterContext.Controller.ViewBag.LeftPanelRouteParam = curTab.LeftPanelAreaName;
                            }
                        }


                        if (!string.IsNullOrEmpty(curTab.RightPanelActionName))
                        {
                            filterContext.Controller.ViewBag.RightPanelActionName = curTab.RightPanelActionName;
                            filterContext.Controller.ViewBag.RightPanelControllerName = curTab.RightPanelControllerName;
                            if (curTab.RightPanelAreaName.Length > 0)
                            {
                                filterContext.Controller.ViewBag.RightPanelRouteParam = curTab.RightPanelAreaName;
                            }
                        }

                    }

                }

                base.OnActionExecuting(filterContext);
            }

            #region Helper

            private MenuContract GetCurrentTab(int tabId, int tabParent, List<MenuContract> menuList)
            {
                try
                {
                    var thisTab = menuList.FindAll(m => m.PortalTabId == tabId);
                    if (!thisTab.Any()) { return null; }
                    if (thisTab.Count != 1 || thisTab[0].TabParentId != tabParent) { return null; }
                    return thisTab[0];
                }
                catch (Exception ex)
                {
                    ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                    return null;
                }
            }
            private bool LoadTabs(List<MenuContract> menuList, int tabId, int tabParentId, int tabType, ActionExecutingContext filterContext)
            {
                try
                {
                    filterContext.Controller.ViewBag.SubMenuList = new List<MenuContract>();
                    filterContext.Controller.ViewBag.TopMenuList = new List<MenuContract>();

                    if (tabId > 0)
                    {
                        menuList.ForEachx(m =>
                        {
                            if (m.PortalTabId == tabId || m.PortalTabId == tabParentId)
                            {
                                m.IsCurrentlyActive = true;
                            }
                            else
                            {
                                m.IsCurrentlyActive = false;
                            }
                        });
                    }

                    var menuType = (((int)filterContext.Controller.ViewBag.MenuType) > 0) ? (int)filterContext.Controller.ViewBag.MenuType : 1;

                    switch (menuType)
                    {
                        case 1:
                            filterContext.Controller.ViewBag.MenuList = menuList;
                            break;
                        case 2:
                            // ReSharper disable once PossibleNullReferenceException
                            var mTopList = menuList.FindAll(m => m.TabParentId == 1);
                            if (mTopList.IsNullOrEmpty())
                            {
                                return false;
                            }
                            if (tabParentId < 1) //Load a default subtab list
                            {
                                var mList = menuList.FindAll(m => m.TabParentId == 2); //Load default sub tabs for tab id 2 /Profile tab
                                if (mList.Count < 1)
                                {
                                    return false;
                                }
                                var mList2 = new List<MenuContract>();
                                var mList3 = new List<MenuContract>();
                                mList.ForEachx(m => mList2.AddRange(menuList.FindAll(p => p.TabParentId == m.PortalTabId)));
                                mList2.ForEachx(m => mList3.AddRange(menuList.FindAll(p => p.TabParentId == m.PortalTabId)));

                                mList.AddRange(mList2);
                                mList.AddRange(mList3);

                                mList[0].IsCurrentlyActive = true;
                                mTopList[0].IsCurrentlyActive = true;
                                filterContext.Controller.ViewBag.TopMenuList = mTopList;
                                filterContext.Controller.ViewBag.SubMenuList = mList;
                            }
                            else
                            {
                                List<MenuContract> mList;
                                if (tabType > 1) //Subtab was clicked
                                {
                                    mList = menuList.FindAll(m => m.TabParentId == tabParentId);
                                    if (mList.Count < 1)
                                    {
                                        return false;
                                    }
                                }
                                else
                                {
                                    mList = menuList.FindAll(m => m.TabParentId == tabId);
                                    if (mList.Count < 1)
                                    {
                                        return false;
                                    }
                                }

                                var mList2 = new List<MenuContract>();
                                var mList3 = new List<MenuContract>();
                                mList.ForEachx(m => mList2.AddRange(menuList.FindAll(p => p.TabParentId == m.PortalTabId)));
                                mList2.ForEachx(m => mList3.AddRange(menuList.FindAll(p => p.TabParentId == m.PortalTabId)));

                                mList.AddRange(mList2);
                                mList.AddRange(mList3);

                                filterContext.Controller.ViewBag.TopMenuList = mTopList;
                                filterContext.Controller.ViewBag.SubMenuList = mList;
                            }
                            break;
                        case 3:

                            break;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                    return false;
                }
            }
            private string GetImageSrcName(PortalUserContract userProfile, bool pictureBeside, ActionExecutingContext filterContext)
            {
                try
                {
                    if (!pictureBeside) return "Images/Site/avatar2.jpg";
                    if (userProfile == null) return "Images/Site/avatar2.jpg";
                    if (userProfile.Picture == null) return "Images/Site/avatar2.jpg";
                    if (userProfile.Picture.Length <= 4) return "Images/Site/avatar2.jpg";
                    string filexx;
                    if (filterContext.HttpContext.Session != null)
                    {
                        filexx = filterContext.HttpContext.Session.SessionID + "_" + DateTime.Now.ToString("hh_mm_ss") +
                                 "_" + userProfile.UserId + "_profilepic.jpg";
                    }
                    else
                    {
                        filexx = userProfile.FullName.Replace(" ", "").GetHashCode().ToString(CultureInfo.InvariantCulture);
                    }

                    var picPath = UploadManager3.SaveBinaryToPath(filexx, userProfile.Picture);
                    return !string.IsNullOrEmpty(picPath) ? picPath : "Images/Site/avatar2.jpg";
                }
                catch (Exception ex)
                {
                    ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                    return "";
                }

            }

            #endregion
        }

        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
        public sealed class ZAPPAuthenticationAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {

                filterContext.Controller.ViewBag.UserINFOCode = null;
                filterContext.Controller.ViewBag.FirstLogin = null;

                if (!filterContext.Controller.ViewData.ModelState.IsValid)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Provide login information");
                    return;
                }


                var modelList = filterContext.ActionParameters.Where(ap => ap.Key == "model").ToList();
                if (modelList.IsNullOrEmpty())
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid Login Information");
                    return;
                }
                if (!modelList.Any() || modelList.Count != 1)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid Login Information");
                    return;
                }

                var model = modelList[0].Value as UserLoginContract;

                if (model == null)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid Login Information");
                    return;
                }

                if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password))
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Empty / Invalid username or password");
                    return;
                }

                //Validate User
                string msg;
                try
                {
                    if (!UserAuthentication.ValidateUser(model.UserName, model.Password, out msg))
                    {
                        filterContext.Controller.ViewData.ModelState.AddModelError("", msg.Length > 0 ? msg : "Login Failed!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Title:") && ex.Message.IndexOf("|", StringComparison.Ordinal) > -1)
                    {
                        var xx = ExceptionManager.DecodeFriendlyError(ex.Message);
                        if (xx != null)
                        {
                            var errDesc = xx["Description"];
                            filterContext.Controller.ViewData.ModelState.AddModelError("", errDesc.Replace("Title,", ""));
                        }
                        else
                        {
                            filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid login details");
                        }
                        return;
                    }
                    filterContext.Controller.ViewData.ModelState.AddModelError("", ex.Message);

                }


                //Check Multiple Login
                var code = model.UserName.Trim() + model.Password.Trim();
                if (PostLoginOperations.IsMultipleLogin(code, out msg))
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", msg.Length > 0 ? msg : "Unauthorized Access!");
                    return;
                }

                int firstLogin;
                var userId = CheckLogin(model.UserName.Trim(), out firstLogin);

                if (userId < 1)
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", msg.Length > 0 ? msg : "Unauthorized Access!");
                    return;
                }

                var encTicket = new UserAuthentication.FormsAuthenticationService().SignIn(model.UserName, false, userId);
                if (String.IsNullOrEmpty(encTicket))
                {
                    filterContext.Controller.ViewData.ModelState.AddModelError("", "Invalid authentication!");
                    return;
                }

                filterContext.HttpContext.Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, encTicket));

                filterContext.Controller.ViewBag.UserINFOCode = code;
                filterContext.Controller.ViewBag.FirstLogin = firstLogin.ToString(CultureInfo.InvariantCulture);

                base.OnActionExecuting(filterContext);
            }
            #region Helpers
            private int CheckLogin(string userName, out int firstLogin)
            {
                try
                {
                    var myProfile = MvcApplication.MyUserProfile(userName);
                    if (myProfile == null)
                    {
                        firstLogin = 0;
                        return 0;
                    }

                    firstLogin = myProfile.UserFirstLogin;
                    return myProfile.UserId;
                }
                catch (Exception ex)
                {
                    ErrorManager.LogApplicationError(ex.StackTrace, ex.Source, ex.Message);
                    firstLogin = 0;
                    return 0;
                }


            }
            #endregion
        }

        public sealed class ZAPPUserAuthorizationAttribute : ActionFilterAttribute
        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)
            {
                if (!filterContext.HttpContext.Request.IsAjaxRequest())
                { return; }
                filterContext.HttpContext.Response.StatusCode = 600;
                filterContext.Controller.ViewBag.ValidAuthourized = "0";

                var error = new StringBuilder();
                var modelState = filterContext.Controller.ViewData.ModelState;
                if (!modelState.IsValid)
                {
                    var errorModel =
                            from x in modelState.Keys
                            where modelState[x].Errors.Count > 0
                            select new
                            {

                                key = x,
                                errors = modelState[x].Errors.
                                                       Select(y => y.ErrorMessage).
                                                       ToArray()
                            };

                    foreach (var item in errorModel)
                    {
                        error.AppendLine(string.Format("Error Key: {0} Error Message: {1}", item.key, string.Join(",", item.errors)));

                    }

                    filterContext.HttpContext.Response.AppendHeader("message", error.ToString());
                    return;
                }


                var modelList = filterContext.ActionParameters.Where(ap => ap.Key == "model").ToList();
                if (modelList.IsNullOrEmpty())
                {
                    filterContext.HttpContext.Response.AppendHeader("message", "Invalid Registration Information");
                    return;
                }
                if (!modelList.Any() || modelList.Count != 1)
                {
                    filterContext.HttpContext.Response.AppendHeader("message", "Invalid Registration Information");
                    return;
                }

                var model = modelList[0].Value as PortalUserContract;

                if (model == null)
                {
                    filterContext.HttpContext.Response.AppendHeader("message", "Invalid Registration Information");
                    return;
                }

                if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName))
                {
                    filterContext.HttpContext.Response.AppendHeader("message", "Invalid Registration Information");
                    return;
                }


                string msg;
                var retVal = UserAuthentication.RegisterUser(model, out msg);
                if (!retVal)
                {
                    filterContext.HttpContext.Response.AppendHeader("message", msg.Length > 0 ? msg : "Invalid Registration Information");
                    return;
                }
                filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                filterContext.HttpContext.Response.AppendHeader("", "");
                filterContext.Controller.ViewBag.ValidAuthourized = "1";
                base.OnActionExecuting(filterContext);
            }
        }
    }
}