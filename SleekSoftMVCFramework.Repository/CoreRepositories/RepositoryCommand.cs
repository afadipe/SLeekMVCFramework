using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using SleekSoftMVCFramework.Data;
using SleekSoftMVCFramework.Data.EntityContract;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SleekSoftMVCFramework.Repository.CoreRepositories
{
    ///---------------------------------------------------------------------------------------------
    /// <summary>
    /// Implement CQRS(Command Query Repository Patterns)
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
    public class RepositoryCommand<TEntity, TPrimaryKey> : IRepositoryCommand<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        private APPContext _context;
        public RepositoryCommand(APPContext context)
        {
            this._context = context;
        }

        public RepositoryCommand()
        {
            this._context = new APPContext();
        }


        /// <summary>
        /// get the current Entity
        /// <returns><see cref="DbSet{TEntity}"/></returns>
        /// </summary>
        public virtual DbSet<TEntity> Table
        {
            get { return _context.Set<TEntity>(); }
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        //public TPrimaryKey SaveChanges()
        //{
        //    return _context.SaveChanges()(TPrimaryKey);
        //}

        //public Task<TPrimaryKey> SaveChangesAsync()
        //{
        //    return _context.SaveChangesAsync();
        //}

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public TEntity Insert(TEntity entity)
        {
            return Table.Add(entity);
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Table.Add(entity));
        }

        public TPrimaryKey InsertAndGetId(TEntity entity)
        {
            entity = Insert(entity);
            if (entity.IsTransient())
            {
                _context.SaveChanges();
            }

            return entity.Id;
        }

        public async Task<TPrimaryKey> InsertAndGetIdAsync(TEntity entity)
        {
            entity = await InsertAsync(entity);
            if (entity.IsTransient())
            {
                await _context.SaveChangesAsync();
            }

            return entity.Id;
        }

        public TPrimaryKey InsertOrUpdateAndGetId(TEntity entity)
        {
            entity = InsertOrUpdate(entity);
            if (entity.IsTransient())
            {
                _context.SaveChanges();
            }

            return entity.Id;
        }

        public async Task<TPrimaryKey> InsertOrUpdateAndGetIdAsync(TEntity entity)
        {
            entity = await InsertOrUpdateAsync(entity);
            if (entity.IsTransient())
            {
                await _context.SaveChangesAsync();
            }

            return entity.Id;
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            AttachIfNot(entity);
            _context.Entry(entity).State = EntityState.Modified;
            return Task.FromResult(entity);
        }

        public void Delete(TEntity entity)
        {
            Update(entity.Id, x => x.IsDeleted = true);
        }


        public TEntity Update(TPrimaryKey id, Action<TEntity> updateAction)
        {
            var entity = Table.Find(id);
            updateAction(entity);
            return entity;
        }

        public void Delete(TPrimaryKey id)
        {
            Update(id, x => x.IsDeleted = true);
        }

        public Task DeleteAsync(TPrimaryKey id)
        {
            return Task.Run(() => Delete(id));

        }


        public async Task<TEntity> UpdateAsync(TPrimaryKey id, Func<TEntity, Task> updateAction)
        {
            var entity = await Table.FindAsync(id);
            await updateAction(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> predicate, Func<TEntity, Task> updateAction)
        {
            var entity = await Table.FirstOrDefaultAsync(predicate);
            await updateAction(entity);
            return entity;
        }



        public virtual TEntity InsertOrUpdate(TEntity entity)
        {
            return entity.IsTransient()
                ? Insert(entity)
                : Update(entity);
        }

        public virtual async Task<TEntity> InsertOrUpdateAsync(TEntity entity)
        {
            return entity.IsTransient()
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }

        public virtual Task DeleteAsync(TEntity entity)
        {
            Delete(entity);
            return Task.FromResult(0);
        }


        protected virtual void AttachIfNot(TEntity entity)
        {
            if (!Table.Local.Contains(entity))
            {
                Table.Attach(entity);
            }
        }

        #region MyCommandRegion
        public async Task<TEntity> InsertOrUpdateAsync(Expression<Func<TEntity, bool>> predicate, TEntity entity)
        {
            return !Table.Any(predicate) && entity.IsActive== true
                //.Id == 0(TPrimaryKey)
                ? await InsertAsync(entity)
                : await UpdateAsync(entity);
        }



        #endregion
    }
}
