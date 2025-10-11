using ITI_SC_Project.Contexts;
using ITI_SC_Project.Helpers;
using Microsoft.EntityFrameworkCore;

namespace ITI_SC_Project.Repositories
{
    public class GenericRepository<TEntity>(HotelDbContext context) : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbSet<TEntity> dbSet = context.Set<TEntity>();

        public IQueryable<TEntity> GetAll(QueryOptions<TEntity>? queryOptions = null)
        {
            IQueryable<TEntity> query = dbSet;

            if (queryOptions != null)
            {
                if (queryOptions.Filter != null)
                    query = query.Where(queryOptions.Filter);

                foreach (var include in queryOptions.Includes)
                    query = query.Include(include);

                if (queryOptions.OrderBy != null)
                    query = queryOptions.OrderBy(query);
            }

            return query;
        }

        public async Task<TEntity?> GetByIdAsync(object id) => await dbSet.FindAsync(id);

        public async Task AddAsync(TEntity entity) => await dbSet.AddAsync(entity);

        public void Update(TEntity entity) => dbSet.Update(entity);

        public async Task DeleteAsync(object id)
        {
            var entity = await dbSet.FindAsync(id);

            if (entity != null) dbSet.Remove(entity);
        }
    }
}
