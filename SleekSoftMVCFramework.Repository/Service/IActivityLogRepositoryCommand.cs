using System.Threading.Tasks;
using System;
using SleekSoftMVCFramework.Repository;
using SleekSoftMVCFramework.Data.Entities;
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
    public interface IActivityLogRepositoryCommand : IAutoDependencyRegister 
    {
        //: IRepository<ActivityLog>
        Task CreateActivityLogAsync(string descriptn, string controllerName, string actionName, Int64 userid, object record);
        void CreateActivityLog(string descriptn, string controllerName, string actionNAme, Int64 userid, object record);
    }
}
