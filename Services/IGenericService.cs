using ITI_SC_Project.Helpers;
using System.Linq.Expressions;

namespace ITI_SC_Project.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>(QueryOptions<TEntity>? queryOptions = null);

        Task<TViewModel?> GetSingleAsync<TViewModel>(Expression<Func<TEntity, bool>> predicate);

        Task<ServiceResult> CreateAsync<TViewModel>(TViewModel viewModel);

        Task<ServiceResult> UpdateAsync<TViewModel>(TViewModel viewModel);

        Task<ServiceResult> DeleteAsync(object id);
    }
}
