namespace AccounterApplication.Data.Common.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;

    using Models;

    public interface IDeletableUserEntityRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IUserEntity<string>
    {
        IQueryable<TEntity> AllWithDeleted();

        IQueryable<TEntity> AllAsNoTrackingWithDeleted();

        Task<TEntity> GetByIdWithDeletedAsync(params object[] id);

        Task<TEntity> GetByIdWithoutDeletedAsync(string userId, params object[] id);

        void HardDelete(TEntity entity);

        void Undelete(TEntity entity);
    }
}
