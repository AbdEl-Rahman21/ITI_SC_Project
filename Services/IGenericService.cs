namespace ITI_SC_Project.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>();

        Task<TViewModel?> GetByIdAsync<TViewModel>(object id);

        Task CreateAsync<TViewModel>(TViewModel viewModel);

        Task UpdateAsync<TViewModel>(TViewModel viewModel);

        Task DeleteAsync(object id);
    }
}
