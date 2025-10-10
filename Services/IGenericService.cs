using ITI_SC_Project.Helpers;

namespace ITI_SC_Project.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>(QueryOptions<TEntity>? queryOptions = null);

        Task<TViewModel?> GetByIdAsync<TViewModel>(object id);

        Task<ServiceResult> CreateAsync<TViewModel>(TViewModel viewModel);

        Task<ServiceResult> UpdateAsync<TViewModel>(TViewModel viewModel);

        Task<ServiceResult> DeleteAsync(object id);
    }
}
