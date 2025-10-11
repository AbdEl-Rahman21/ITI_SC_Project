using ITI_SC_Project.Helpers;

namespace ITI_SC_Project.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAll(QueryOptions<TEntity>? queryOptions = null);

        Task<TEntity?> GetByIdAsync(object id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        Task DeleteAsync(object id);
    }
}
