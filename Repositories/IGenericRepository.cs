using ITI_SC_Project.Helpers;
using System.Linq.Expressions;

namespace ITI_SC_Project.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(QueryOptions<TEntity>? queryOptions = null);

        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> predicate);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        Task DeleteAsync(object id);
    }
}
