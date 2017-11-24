using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SleekSoftMVCFramework.Data.EntityBase;

namespace SleekSoftMVCFramework.Data.Entities
{
    ///---------------------------------------------------------------------------------------------
    /// <summary>
    /// Defines Activitylog for managing all activities that happen throughout the life cycle of an aplication
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
    public class ActivityLog : Entity<long>
    {

        public Int64? UserId { get; set; }

        public string ModuleName { get; set; }
        public string ModuleAction { get; set; }

        public string Description { get; set; }

        public string Record { get; set; }

    }
}
