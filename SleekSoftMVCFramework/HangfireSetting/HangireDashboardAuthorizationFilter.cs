using Hangfire.Dashboard;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SleekSoftMVCFramework.HangfireSetting
{
    ///---------------------------------------------------------------------------------------------
    /// <summary>
    ///  Hangire Dashboard Authorization Filter class for authenticating user with the right role to view Hnagfire dashboard
    /// </summary>
    /// <copyright>
    /// *****************************************************************************
    ///     ----- Fadipe Wasiu Ayobami . All Rights Reserved. Copyright (c) 2017
    /// *****************************************************************************
    /// </copyright>
    /// <remarks>
    /// *****************************************************************************
    ///     ---- Created For: Public Use (All Products)
    ///     ---- Created By: Fadipe Wasiu Ayobami
    ///     ---- Original Language: C#
    ///     ---- Current Version: v1.0.0.0.1
    ///     ---- Current Language: C#
    /// *****************************************************************************
    /// </remarks>
    /// <history>
    /// *****************************************************************************
    ///     --- Date First Created : 08 - 11 - 2017
    ///     --- Author: Fadipe Wasiu Ayobami
    ///     --- Date First Reviewed: 
    ///     --- Date Last Reviewed:
    /// *****************************************************************************
    /// </history>
    /// <usage>
    /// Class a new class i.e Animal
    /// public class Animal:BaseEntityWithAudit<int>
    /// {
    /// 
    /// }
    /// 
    /// -- Fadipe Wasiu Ayobami
    /// </usage>
    /// ----------------------------------------------------------------------------------------------
    ///
    public class HangireDashboardAuthorizationFilter: IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            var owinContext = new OwinContext(context.GetOwinEnvironment());
            // Allow all authenticated users to see the Dashboard (potentially dangerous).
           if(owinContext.Authentication.User.Identity.IsAuthenticated)
                return owinContext.Authentication.User.IsInRole("PortalAdmin");
            return false;

        }
    }
}