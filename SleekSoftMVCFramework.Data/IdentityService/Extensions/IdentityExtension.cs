
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNet.Identity;

namespace SleekSoftMVCFramework.Data.IdentityService.Extensions
{
    ///---------------------------------------------------------------------------------------------
    /// <summary>
    /// Defines IdentityExtension for managing IdentityClaim setting
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
    public static class IdentityExtension
    {
        public static string GetFullName(this IIdentity identity)
        {

            var claim = ((ClaimsIdentity)identity);
            var fullname = claim.FindFirstValue("FullName");
            // Test for null to avoid issues during local testing
            return fullname ?? string.Empty;
        }
    }
}
