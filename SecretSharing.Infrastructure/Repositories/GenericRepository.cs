using Microsoft.EntityFrameworkCore;
using SecretSharing.Core.Interfaces;
using SecretSharing.Core.Specifications;
using SecretSharing.Infrastructure.Data;

namespace SecretSharing.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            try
            {
                return await _storeContext.Set<T>().ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<T> GetByIdAsync(string id)
        {
            try
            {
                return await _storeContext.Set<T>().FindAsync(new Guid(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            try
            {
                var entity = await GetByIdAsync(id);
                if (entity != null)
                {
                    Delete(entity);
                    return true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            return false;
        }

        public void Add(T entity)
        {
            _storeContext.Add(entity);
        }

        public void Delete(T entity)
        {
            _storeContext.Set<T>().Remove(entity);
        }
        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> specification)
        {
            return await ApplySpecification(specification).ToListAsync();

        }
        private IQueryable<T> ApplySpecification(ISpecification<T> specifications)
        {
            return SpecificationEvaluateOr<T>.GetQuery(_storeContext.Set<T>().AsQueryable(), specifications);
        }

    }
}
