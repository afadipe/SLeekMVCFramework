using System;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Security.Claims;
using SleekSoftMVCFramework.Data.EntityContract;

namespace SleekSoftMVCFramework.Data.IdentityModel
{
    ///---------------------------------------------------------------------------------------------
    /// <summary>
    /// Defines ApplicationUser which implement ASP.Net Identity 2  ApplicationUser interface and override it.
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
    public class ApplicationUser : IdentityUser<long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IAduit<Int64>
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, long> manager)
        {
            // Note the authenticationType must match the one defined in 
            // CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            userIdentity.AddClaim(new Claim("FullName", FirstName + " " + MiddleName + " " + LastName));

            return userIdentity;
        }
        public ApplicationUser()
        {
            DateCreated = DateTime.Now;
            this.IsFirstLogin = true;
            this.IsDeleted = false;
            this.IsActive = true;
        }
        public ApplicationUser(string username)
        {
            this.UserName = username;
        }
        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        public string LastName { get; set; }

        public DateTime? DOB { get; set; }

        public string MobileNumber { get; set; }

        public string Address { get; set; }

        public bool IsFirstLogin { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return this.LastName + " " + this.FirstName;
            }
        }


        [Timestamp]
        public byte[] RowVersion { get; set; }

        public bool IsTransient()
        {
            return true;
            //return EqualityComparer<Int64>.Default.Equals(long, default(Int64));
        }

        public Int64 CreatedBy { get; set; }

        public Int64 DeletedBy { get; set; }

        public Int64 UpdatedBy { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }


    }
}
