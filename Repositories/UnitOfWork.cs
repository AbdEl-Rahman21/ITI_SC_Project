using ITI_SC_Project.Contexts;

namespace ITI_SC_Project.Repositories
{
    public class UnitOfWork(HotelDbContext context, IServiceProvider serviceProvider) : IUnitOfWork
    {
        private readonly HotelDbContext context = context;
        private readonly IServiceProvider serviceProvider = serviceProvider;
        private readonly Dictionary<Type, object> repositories = [];

        private bool disposed = false;

        public TRepository GetRepository<TRepository>() where TRepository : notnull
        {
            var type = typeof(TRepository);

            if (repositories.TryGetValue(type, out object? value)) return (TRepository)value;

            var repository = serviceProvider.GetRequiredService<TRepository>();

            repositories[type] = repository;

            return repository;
        }

        public async Task<int> SaveAsync() => await context.SaveChangesAsync();

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
