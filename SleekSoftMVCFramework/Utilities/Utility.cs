using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using SleekSoftMVCFramework.Data.IdentityModel;
using SleekSoftMVCFramework.Repository;
using SleekSoftMVCFramework.Repository.CoreRepositories;
using SleekSoftMVCFramework.Data.IdentityService;

namespace SleekSoftMVCFramework.Utilities
{
    public class Utility
    {
        private readonly IRepositoryQuery<Permission,long> _permissionQuery;
        private readonly IRepositoryQuery<ApplicationRole, long> _applicationRoleQuery;
        private readonly ApplicationRoleManager _applicationRoleManager;
        public Utility(IRepositoryQuery<Permission, long> permissionQuery, IRepositoryQuery<ApplicationRole, long> applicationRoleQuery, ApplicationRoleManager applicationRoleManager)
        {
            _permissionQuery = permissionQuery;
            _applicationRoleManager = applicationRoleManager;
            _applicationRoleQuery = applicationRoleQuery;
        }
        public IEnumerable<SelectListItem> GetPermissions()
        {
            var types = _permissionQuery.GetAllList().Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Id.ToString(),
                                    Text = x.Name

                                }).AsEnumerable();
            return new SelectList(types, "Value", "Text");
        }


        public IEnumerable<SelectListItem> GetRoles()
        {
            var types = _applicationRoleQuery.GetAllList().Select(x =>
                                new SelectListItem
                                {
                                    Value = x.Name,
                                    Text = x.Name

                                }).AsEnumerable();
            return new SelectList(types, "Value", "Text");
        }


        public void SendPasswordResetEmail(ApplicationUser mUser, string mtoken, string resetUrl)
        {
            try
            {
                //EmailTemplate emailFormat = _emailTemplateRepositoryRepositoryQuery.GetByCode("F_PASSWORD");
                //List<EmailToken> tokenCol = _emailTokenRepositoryQuery.GetAllList(m => m.EmailCode == emailFormat.Code).ToList();
                //foreach (var token in tokenCol)
                //{
                //    if (token.Token.Equals("{Name}"))
                //    {
                //        token.PreviewText = mUser.FirstName + " " + mUser.LastName;
                //    }
                //    else if (token.Token.Equals("{Email}"))
                //    {
                //        token.PreviewText = mUser.Email ?? string.Empty;
                //    }
                //    else if (token.Token.Equals("{PFNumber}"))
                //    {
                //        token.PreviewText = mUser.PFNo ?? string.Empty;
                //    }
                //    else if (token.Token.Equals("{Url}"))
                //    {
                //        ////we need to get portalUrl bcos of live n local host will both b different
                //        //string portalUrl = HttpContext.Current.Request.Url.Scheme + "://" + HttpContext.Current.Request.Url.Authority + HttpContext.Current.Request.ApplicationPath.TrimEnd('/') + "/";
                //        //string mPre = portalUrl + resetUrl;
                //        //string URL = string.Format(mPre, mtoken);
                //        token.PreviewText = resetUrl;
                //    }
                //}
                //try
                //{
                //    EmailLog mlog = new EmailLog();
                //    mlog.BCC = "a@gmail.com";
                //    mlog.CC = "a@gmail.com";
                //    mlog.Receiver = mUser.Email;
                //    mlog.Sender = GetAppSetting("EmailSender");
                //    mlog.Subject = "Password reset notification";
                //    mlog.MessageBody = GeneratePreviewHTML(emailFormat.Body, tokenCol);
                //    mlog.ApplicationID = GetIntAppSetting("EmailEngineAppId");
                //    mlog.DateCreated = mlog.DateToSend = DateTime.Now;
                //    mlog.IsSent = mlog.HasAttachment = false;
                //    mlog.EmailAttachments = new List<EmailAttachment>();
                //    _EmailEngineModel.EmailLogs.Add(mlog);
                //    _EmailEngineModel.SaveChanges();
                //}
                //catch (DbEntityValidationException filterContext)
                //{
                //    if (typeof(DbEntityValidationException) == filterContext.GetType())
                //    {
                //        foreach (var validationErrors in filterContext.EntityValidationErrors)
                //        {
                //            foreach (var validationError in validationErrors.ValidationErrors)
                //            {
                //                System.Diagnostics.Debug.WriteLine("Property: {0} Error: {1}",
                //                    validationError.PropertyName,
                //                    validationError.ErrorMessage);

                //            }
                //        }
                //    }
                //    throw;
                //}

            }
            catch (Exception)
            {

                throw;
            }
        }


        public IEnumerable<SelectListItem> GetSpecificRoles(Int64[] roleIds)
        {

            var types = _applicationRoleManager.Roles.Where(e =>roleIds.Contains(e.Id)).Select(x =>
                                  new SelectListItem
                                  {
                                      Value = x.Id.ToString(),
                                      Text = x.Name
                                  });

            return new SelectList(types, "Value", "Text");
        }
    }
}