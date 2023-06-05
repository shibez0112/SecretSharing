using SecretSharing.Core.Specifications;

namespace SecretSharing.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        void Add(T entity);
        void Delete(T entity);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(string id);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
        Task<bool> DeleteByIdAsync(string id);
    }
}