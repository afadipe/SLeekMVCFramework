using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleekSoftMVCFramework.Data.IdentityModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace SleekSoftMVCFramework.Data.IdentityService
{
    ///---------------------------------------------------------------------------------------------
    /// <summary>
    /// Defines ApplicationUserManager which implement ASP.Net Identity 2   UserManager
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
    /// 
    /// -- Fadipe Wasiu Ayobami
    /// </usage>
    /// ----------------------------------------------------------------------------------------------
    ///
    ///

    public class ApplicationUserManager: UserManager<ApplicationUser,Int64>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser,Int64> store) : base(store)
        {
        }
    }
}
