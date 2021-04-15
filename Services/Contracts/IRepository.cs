using System;
using System.Linq;
using System.Linq.Expressions;

namespace CustomerAPI.Services
{
    /// <summary>
    /// Interface of repository
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The TEntity</returns>
        TEntity Create(TEntity entity);

        /// <summary>
        /// Finds all.
        /// </summary>
        /// <returns cref="IQueryable{TEntity}">The queryable of entity.</returns>
        IQueryable<TEntity> FindAll();

        /// <summary>
        /// Finds the by condition.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns cref="IQueryable{TEntity}">The queryable of entity.</returns>
        IQueryable<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns cref="TEntity">The TEntity.</returns>
        TEntity Update(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        void Delete(TEntity entity);
    }
}
