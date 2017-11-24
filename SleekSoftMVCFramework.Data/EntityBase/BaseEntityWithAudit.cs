using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SleekSoftMVCFramework.Data.EntityContract;
using SleekSoftMVCFramework.Data.IdentityModel;

namespace SleekSoftMVCFramework.Data.EntityBase
{
    ///---------------------------------------------------------------------------------------------
    /// <summary>
    ///   Is a BaseEntityClass which inherit for AuditEntity to aid fast development
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
    public class BaseEntityWithAudit<TPrimaryKey> : IAduit<TPrimaryKey>
    {
        public BaseEntityWithAudit()
        {
            IsActive = true;
            DateCreated=DateTime.Now;
            IsDeleted = false;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public TPrimaryKey Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsActive { get; set; }
        public Int64 CreatedBy { get; set; }
        public Int64 UpdatedBy { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; } 

        [Timestamp]
        public byte[] RowVersion { get; set; }
        public bool IsTransient()
        {
            return EqualityComparer<TPrimaryKey>.Default.Equals(Id, default(TPrimaryKey));
            
        }
    }
}