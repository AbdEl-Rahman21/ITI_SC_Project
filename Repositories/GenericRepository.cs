using ITI_SC_Project.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ITI_SC_Project.Repositories
{
    public class GenericRepository<T>(HotelDbContext context) : IGenericRepository<T> where T : class
    {
        private readonly DbSet<T> dbSet = context.Set<T>();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            if (filter != null) query = query.Where(filter);

            foreach (var include in includes) query = query.Include(include);

            if (orderBy != null) query = orderBy(query);

            return await query.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(object id) => await dbSet.FindAsync(id);

        public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);

        public void Update(T entity) => dbSet.Update(entity);

        public void Delete(T entity) => dbSet.Remove(entity);
    }
}
