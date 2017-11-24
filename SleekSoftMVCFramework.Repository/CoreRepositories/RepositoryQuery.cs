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
using LinqKit;
using SleekSoftMVCFramework.Repository.QueryRepository;

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
    public class RepositoryQuery<TEntity, TPrimaryKey> : IRepositoryQuery<TEntity, TPrimaryKey> where TEntity : class, IEntity<TPrimaryKey>
    {
        private APPContext _context;
        public RepositoryQuery(APPContext context)
        {
            this._context = context;
        }

        public RepositoryQuery()
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

        /// <summary>
        /// get the entity <see cref="IQueryable{TEntity}"/>
        /// </summary>
        /// <returns></returns>
        public  IQueryable<TEntity> GetAll()
        {
            return Table.AsNoTracking().Where(x => !x.IsDeleted && x.IsActive);
        }



        public virtual List<TEntity> GetAllList()
        {
            return GetAll().ToList();
        }


        public virtual Task<List<TEntity>> GetAllListAsync()
        {
            return Task.FromResult(GetAllList());
        }

        public virtual List<TEntity> GetAllList(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).ToList();
        }

        public virtual Task<List<TEntity>> GetAllListAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(GetAllList(predicate));
        }

        public virtual T Query<T>(Func<IQueryable<TEntity>, T> queryMethod)
        {
            return queryMethod(GetAll());
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
            var entity = FirstOrDefault(id);
            if (entity == null)
            {
                throw new Exception("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }

            return entity;
        }

        public virtual async Task<TEntity> GetAsync(TPrimaryKey id)
        {
            var entity = await FirstOrDefaultAsync(id);
            if (entity == null)
            {
                throw new Exception("There is no such an entity with given primary key. Entity type: " + typeof(TEntity).FullName + ", primary key: " + id);
            }

            return entity;
        }

        public virtual TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Single(predicate);
        }

        public virtual Task<TEntity> SingleAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Single(predicate));
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return GetAll().FirstOrDefault(CreateEqualityExpressionForId(id));
        }

        public virtual Task<TEntity> FirstOrDefaultAsync(TPrimaryKey id)
        {
            return Task.FromResult(FirstOrDefault(id));
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }
        public virtual TEntity LastOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().LastOrDefault(predicate);
        }

        public virtual Task<TEntity> LastOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LastOrDefault(predicate));
        }
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(FirstOrDefault(predicate));
        }

        public virtual TEntity Load(TPrimaryKey id)
        {
            return Get(id);
        }


        public virtual int Count()
        {
            return GetAll().Count();
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).Count();
        }

        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(Count(predicate));
        }

        public virtual long LongCount()
        {
            return GetAll().LongCount();
        }

        public virtual Task<long> LongCountAsync()
        {
            return Task.FromResult(LongCount());
        }

        public virtual long LongCount(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().Where(predicate).LongCount();
        }

        public virtual Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return Task.FromResult(LongCount(predicate));
        }

        #region MyRegion


        public IQueryFluent<TEntity> Query()
        {
            return new QueryFluent<TEntity, TPrimaryKey>(this);
        }

        public virtual IQueryFluent<TEntity> Query(IQueryObject<TEntity> queryObject)
        {
            return new QueryFluent<TEntity, TPrimaryKey>(this, queryObject);
        }

        public virtual IQueryFluent<TEntity> Query(Expression<Func<TEntity, bool>> query)
        {
            return new QueryFluent<TEntity, TPrimaryKey>(this, query);
        }

        public IQueryable<TEntity> Queryable()
        {
            return Table;
        }
        #endregion


        #region MyStoredProdureRegion

        public virtual IQueryable<TEntity> SelectQuery(string query, params object[] parameters)
        {
            return Table.SqlQuery(query, parameters).AsQueryable();
        }

        public DbRawSqlQuery<T> ExecuteStoredProdure<T>(string storedProcedureNameAndParameterPlaceholder, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>("EXEC " + storedProcedureNameAndParameterPlaceholder, parameters);
        }

        public void ExecuteStoreprocedure(string storedProcedureNameAndParameterPlaceholder, params object[] parameters)
        {
            _context.Database.ExecuteSqlCommand("EXEC " + storedProcedureNameAndParameterPlaceholder, parameters);
        }

        public DbRawSqlQuery<T> StoreprocedureQueryFor<T>(string storeprocedureName, params object[] parameters)
        {
            return _context.Database.SqlQuery<T>("EXEC " + storeprocedureName, parameters);
        }

        public DbRawSqlQuery<T> StoreprocedureQuery<T>(string storeprocedureName)
        {
            return _context.Database.SqlQuery<T>("EXEC " + storeprocedureName);
        }
        #endregion

        #region MySelectRegion
        internal IQueryable<TEntity> Select(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>> includes = null,
           int? page = null,
           int? pageSize = null)
        {
            IQueryable<TEntity> query = Table;

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            if (filter != null)
            {
                query = query.AsExpandable().Where(filter);
            }
            if (page != null && pageSize != null)
            {
                query = query.Skip((page.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }
            return query;
        }



        internal async Task<IEnumerable<TEntity>> SelectAsync(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           List<Expression<Func<TEntity, object>>> includes = null,
           int? page = null,
           int? pageSize = null)
        {
            return await Select(filter, orderBy, includes, page, pageSize).ToListAsync();
        }
        #endregion

        protected static Expression<Func<TEntity, bool>> CreateEqualityExpressionForId(TPrimaryKey id)
        {
            var lambdaParam = Expression.Parameter(typeof(TEntity));

            var lambdaBody = Expression.Equal(
                Expression.PropertyOrField(lambdaParam, "Id"),
                Expression.Constant(id, typeof(TPrimaryKey))
                );

            return Expression.Lambda<Func<TEntity, bool>>(lambdaBody, lambdaParam);
        }

    }
}