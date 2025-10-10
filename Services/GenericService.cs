using AutoMapper;
using ITI_SC_Project.Helpers;
using ITI_SC_Project.Repositories;

namespace ITI_SC_Project.Services
{
    public class GenericService<TEntity>(IMapper mapper, IUnitOfWork unitOfWork) : IGenericService<TEntity> where TEntity : class
    {
        private readonly IMapper mapper = mapper;
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IGenericRepository<TEntity> repository = unitOfWork.GetRepository<IGenericRepository<TEntity>>();

        public async Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>(QueryOptions<TEntity>? queryOptions = null)
        {
            var entities = await repository.GetAllAsync(queryOptions);

            return mapper.Map<IEnumerable<TViewModel>>(entities);
        }

        public async Task<TViewModel?> GetByIdAsync<TViewModel>(object id)
        {
            var entity = await repository.GetByIdAsync(id);

            return mapper.Map<TViewModel>(entity);
        }

        public async Task CreateAsync<TViewModel>(TViewModel viewModel)
        {
            var entity = mapper.Map<TEntity>(viewModel);

            await repository.AddAsync(entity);

            await unitOfWork.SaveAsync();
        }

        public async Task UpdateAsync<TViewModel>(TViewModel viewModel)
        {
            var entity = mapper.Map<TEntity>(viewModel);

            repository.Update(entity);

            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(object id)
        {
            await repository.DeleteAsync(id);

            await unitOfWork.SaveAsync();
        }
    }
}
