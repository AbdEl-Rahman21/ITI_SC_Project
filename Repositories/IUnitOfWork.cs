namespace ITI_SC_Project.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveAsync();

        TRepository GetRepository<TRepository>() where TRepository : notnull;
    }
}
