using SecretSharing.Core.Interfaces;
using SecretSharing.Infrastructure.Repositories;
using System.Collections;

namespace SecretSharing.Infrastructure.Data
{
    public class UnitOfWork
    {
        private readonly StoreContext _storeContext;
        private Hashtable _repositories;

        public UnitOfWork(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }
        public async Task<int> Complete()
        {
            return await _storeContext.SaveChangesAsync();
        }
        public void Dispose()
        {
            _storeContext.Dispose();
        }
        public IGenericRepository<TEntity> repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) { _repositories = new Hashtable(); }
            var Type = typeof(TEntity);
            if (!_repositories.ContainsKey(Type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _storeContext);
                _repositories.Add(Type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[Type];
        }
    }
}
