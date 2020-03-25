namespace AccounterApplication.Data.Common.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IDeletableEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        Task<TEntity> GetByIdWithDeletedAsync(params object[] id);

        Task<TEntity> GetByIdWithoutDeletedAsync(params object[] id);

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
