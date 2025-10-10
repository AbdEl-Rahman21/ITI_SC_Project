using ITI_SC_Project.Helpers;

namespace ITI_SC_Project.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync(QueryOptions<TEntity>? queryOptions = null);

        Task<TEntity?> GetByIdAsync(object id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        Task DeleteAsync(object id);
    }
}
