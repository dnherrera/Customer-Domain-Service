using System;
using System.Linq;
using System.Linq.Expressions;
using CustomerAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CustomerAPI.Services
{
    /// <summary>
    /// Base repository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <seealso cref="Kob.Uco.Domains.Balances.Repositories.BaseLogger" />
    /// <seealso cref="Kob.Uco.Domains.Balances.Interfaces.Repositories.IRepository{TEntity}" />
    public abstract class BaseRepository<TEntity> : BaseLogger, IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// The database context
        /// </summary>
        protected readonly RepositoryDbContext DbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="repositoryDbContext">The repository database context.</param>
        protected BaseRepository(ILogger logger, RepositoryDbContext repositoryDbContext) : base(logger)
        {
            DbContext = repositoryDbContext;
        }

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns cref="TEntity">Generic TEntity</returns>
        public TEntity Create(TEntity entity)
        {
            DbContext.Set<TEntity>().Add(entity);
            return entity;
        }

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
        }

        /// <summary>
        /// Finds the by condition.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns cref="TEntity">The entity.</returns>
        public IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>().Where(predicate).AsNoTracking();
        }

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns cref="TEntity">The entity.</returns>
        public IQueryable<TEntity> FindAll()
        {
            return DbContext.Set<TEntity>().AsNoTracking();
        }

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns cref="TEntity">Generic TEntity</returns>
        public TEntity Update(TEntity entity)
        {
            return DbContext.Set<TEntity>().Update(entity).Entity;
        }
    }
}
