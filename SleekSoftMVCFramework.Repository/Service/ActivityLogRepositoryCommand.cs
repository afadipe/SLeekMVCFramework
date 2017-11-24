using System;
using System.Threading.Tasks;
using SleekSoftMVCFramework.Data.Entities;
using Newtonsoft.Json;
using log4net;
using SleekSoftMVCFramework.Repository.CoreRepositories;

namespace SleekSoftMVCFramework.Repository
{
    ///---------------------------------------------------------------------------------------------
    /// <summary>
    /// Define  ActivityLog
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
    public class ActivityLogRepositoryCommand :IActivityLogRepositoryCommand
    {
         private readonly IRepositoryCommand<ActivityLog, long> _activityLogRepositoryCommand;
         private readonly ILog _log;
         public ActivityLogRepositoryCommand(IRepositoryCommand<ActivityLog, long> activityLogRepositoryCommand,ILog log)
         {
             _activityLogRepositoryCommand=activityLogRepositoryCommand;
            _log = log;
         }
        
         public async Task CreateActivityLogAsync(string descriptn, string moduleName, string moduleAction, Int64 userid, object record)
         {

                 try
                 {
                     ActivityLog alog = new ActivityLog
                     {

                         ModuleName = moduleName,
                         ModuleAction = moduleAction,
                         UserId = userid,
                         Description = descriptn,
                         Record = record!= null ?JsonConvert.SerializeObject(record):"N/A"
                     };
                     await _activityLogRepositoryCommand.InsertAsync(alog);
                     await _activityLogRepositoryCommand.SaveChangesAsync();
                 }
                 catch (Exception ex)
                 {

                   _log.Error(ex);

                 }
                
             
         }
         public void CreateActivityLog(string descriptn, string moduleName, string moduleAction, Int64 userid, object record)
         {
          
             try
             {
                ActivityLog alog = new ActivityLog
                {
                    ModuleName = moduleName,
                    ModuleAction = moduleAction,
                    UserId = userid,
                    Description = descriptn,
                    Record = record != null ? JsonConvert.SerializeObject(record) : "N/A"

                };
                _activityLogRepositoryCommand.Insert(alog);
                 _activityLogRepositoryCommand.SaveChanges();
             }
             catch (Exception ex)
             {
                _log.Error(ex);
            }
             
         }

         
    }
}
