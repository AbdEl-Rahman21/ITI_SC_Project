using AutoMapper;
using AutoMapper.QueryableExtensions;
using ITI_SC_Project.Helpers;
using ITI_SC_Project.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ITI_SC_Project.Services
{
    public class GenericService<TEntity>(IMapper mapper, IUnitOfWork unitOfWork) : IGenericService<TEntity> where TEntity : class
    {
        private readonly IMapper mapper = mapper;
        private readonly IUnitOfWork unitOfWork = unitOfWork;
        private readonly IGenericRepository<TEntity> repository = unitOfWork.GetRepository<IGenericRepository<TEntity>>();

        public async Task<IEnumerable<TViewModel>> GetAllAsync<TViewModel>(QueryOptions<TEntity>? queryOptions = null)
        {
            var query = repository.GetAll(queryOptions);

            return await query.ProjectTo<TViewModel>(mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<TViewModel?> GetByIdAsync<TViewModel>(object id)
        {
            var entity = await repository.GetByIdAsync(id);

            return mapper.Map<TViewModel>(entity);
        }

        public async Task<ServiceResult> CreateAsync<TViewModel>(TViewModel viewModel)
        {
            try
            {
                var entity = mapper.Map<TEntity>(viewModel);

                await repository.AddAsync(entity);

                await unitOfWork.SaveAsync();

                return ServiceResult.Ok("Entity added successfully.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return ServiceResult.Fail("A concurrency conflict occurred while adding the entity. Please refresh and try again.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx)
                {
                    return sqlEx.Number switch
                    {
                        2627 or 2601 => ServiceResult.Fail("Duplicate entry detected. The value must be unique."),
                        547 => ServiceResult.Fail("Invalid reference. Please ensure related data exists before adding this item."),
                        _ => ServiceResult.Fail("A database error occurred while adding the entity."),
                    };
                }

                return ServiceResult.Fail("Unable to save changes. Please check your input and try again.");
            }
            catch (InvalidOperationException ex)
            {
                return ServiceResult.Fail($"Invalid operation: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult.Fail($"Invalid argument: {ex.Message}");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"An unexpected error occurred: {ex.Message}");
            }
        }

        public async Task<ServiceResult> UpdateAsync<TViewModel>(TViewModel viewModel)
        {
            try
            {
                var entity = mapper.Map<TEntity>(viewModel);

                repository.Update(entity);

                await unitOfWork.SaveAsync();

                return ServiceResult.Ok("Entity updated successfully.");
            }
            catch (DbUpdateConcurrencyException)
            {
                return ServiceResult.Fail("This record was modified or deleted by another user. Please refresh and try again.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx)
                {
                    return sqlEx.Number switch
                    {
                        2627 or 2601 => ServiceResult.Fail("Duplicate entry detected. Another record already uses this value."),
                        547 => ServiceResult.Fail("Invalid reference detected. Please check related records before updating."),
                        _ => ServiceResult.Fail("A database error occurred while updating the entity."),
                    };
                }

                return ServiceResult.Fail("Unable to update entity. Please check your data and try again.");
            }
            catch (InvalidOperationException ex)
            {
                return ServiceResult.Fail($"Invalid operation: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult.Fail($"Invalid argument: {ex.Message}");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"An unexpected error occurred while updating: {ex.Message}");
            }
        }

        public async Task<ServiceResult> DeleteAsync(object id)
        {
            try
            {
                await repository.DeleteAsync(id);

                await unitOfWork.SaveAsync();

                return ServiceResult.Ok("Entity deleted successfully.");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlEx)
                {
                    return sqlEx.Number switch
                    {
                        547 => ServiceResult.Fail("This record cannot be deleted because it is referenced by other data."),
                        2627 or 2601 => ServiceResult.Fail("Duplicate key constraint encountered unexpectedly during delete."),
                        _ => ServiceResult.Fail("A database error occurred while deleting the entity."),
                    };
                }

                return ServiceResult.Fail("Unable to delete entity. Please try again.");
            }
            catch (InvalidOperationException ex)
            {
                return ServiceResult.Fail($"Invalid operation: {ex.Message}");
            }
            catch (ArgumentException ex)
            {
                return ServiceResult.Fail($"Invalid argument: {ex.Message}");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail($"An unexpected error occurred while deleting: {ex.Message}");
            }
        }
    }
}
