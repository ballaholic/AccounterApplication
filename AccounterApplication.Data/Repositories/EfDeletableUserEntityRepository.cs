namespace AccounterApplication.Data.Repositories
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;

    using Infrastructure;
    using Common.Models;
    using Common.Repositories;

    public class EfDeletableUserEntityRepository<TEntity> : EfRepository<TEntity>, IDeletableUserEntityRepository<TEntity>
        where TEntity : class, IDeletableEntity, IUserEntity<string>
    {
        public EfDeletableUserEntityRepository(AccounterDbContext context) 
            : base (context)
        {
        }

        public override IQueryable<TEntity> All() => base.All().Where(x => !x.IsDeleted);

        public override IQueryable<TEntity> AllAsNoTracking() => base.AllAsNoTracking().Where(x => !x.IsDeleted);

        public IQueryable<TEntity> AllWithDeleted() => base.All().IgnoreQueryFilters();

        public IQueryable<TEntity> AllAsNoTrackingWithDeleted() => base.AllAsNoTracking().IgnoreQueryFilters();

        public Task<TEntity> GetByIdWithDeletedAsync(params object[] id)
        {
            var getByIdPredicate = EfExpressionHelper.BuildByIdPredicate<TEntity>(this.Context, id);
            return this.AllWithDeleted().FirstOrDefaultAsync(getByIdPredicate);
        }

        public Task<TEntity> GetByIdWithoutDeletedAsync(string userId, params object[] id)
        {
            var getByIdPredicate = EfExpressionHelper.BuildByIdPredicate<TEntity>(this.Context, id);
            return this.All()
                        .Where(x => x
                                .UserId.Equals(userId))
                        .FirstOrDefaultAsync(getByIdPredicate);
        }

        public void HardDelete(TEntity entity) => base.Delete(entity);

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            this.Update(entity);
        }

        public override void Delete(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.UtcNow;
            this.Update(entity);
        }
    }
}
