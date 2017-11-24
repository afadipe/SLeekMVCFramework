using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SleekSoftMVCFramework.Data.EntityContract
{
    ///---------------------------------------------------------------------------------------------
    /// <summary>
    ///   Is a IAduit class which inherit for IEntity to aid fast development
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
    /// public class Animal:IAduit<int>
    /// {
    /// 
    /// }
    /// 
    /// -- Fadipe Wasiu Ayobami
    /// </usage>
    /// ----------------------------------------------------------------------------------------------
    ///
    public interface IAduit<TPrimaryKey> : IEntity<TPrimaryKey>
    {
        /// <summary>
        /// 
        /// </summary>
        Int64 CreatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
       // Int64 DeletedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
        Int64 UpdatedBy { get; set; }
        /// <summary>
        /// 
        /// </summary>
     
        /// <summary>
        /// 
        /// </summary>
        DateTime? DateUpdated { get; set; }

        /// <summary>
        /// to manage versioning
        /// </summary>
        byte[] RowVersion { get; set; }
    

         
    }
}
