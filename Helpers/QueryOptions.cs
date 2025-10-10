using System.Linq.Expressions;

namespace ITI_SC_Project.Helpers
{
    public class QueryOptions<TEntity>
    {
        public Expression<Func<TEntity, bool>>? Filter { get; set; }
        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? OrderBy { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = [];
    }
}
