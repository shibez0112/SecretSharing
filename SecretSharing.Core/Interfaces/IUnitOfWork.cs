namespace SecretSharing.Core.Interfaces
{
    public interface IUnitOfWork
    {
        Task<int> Complete();
        void Dispose();
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : class;
    }
}