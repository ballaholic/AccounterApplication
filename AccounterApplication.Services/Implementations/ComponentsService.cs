namespace AccounterApplication.Services.Implementations
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Contracts;
    using Data.Models;
    using Services.Mapping;
    using Common.Enumerations;
    using Data.Common.Repositories;

    public class ComponentsService : IComponentsService
    {
        private readonly IDeletableUserEntityRepository<Component> componentsRepository;

        public ComponentsService(IDeletableUserEntityRepository<Component> componentsRepository)
            => this.componentsRepository = componentsRepository;

        public async Task<IEnumerable<T>> AllByUserId<T>(string userId)
            => await this.componentsRepository
                .All()
                .Where(c => c.UserId.Equals(userId))
                .OrderByDescending(c => c.CreatedOn)
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllByUserIdLocalized<T>(string userId, Languages language)
        => await this.componentsRepository
                .All()
                .Where(c => c.UserId.Equals(userId))
                .OrderByDescending(c => c.CreatedOn)
                .To<T>(new { language })
                .ToListAsync();

        public async Task<IEnumerable<T>> AllByUserIdActiveLocalized<T>(string userId, Languages language)
            => await this.componentsRepository
                .All()
                .Where(c => c.UserId.Equals(userId) && c.IsActive)
                .OrderByDescending(c => c.CreatedOn)
                .To<T>(new { language })
                .ToListAsync();

        public async Task<IEnumerable<T>> AllByUserIdAndTypeIdLocalized<T>(string userId, int typeId, Languages language)
            => await this.componentsRepository
                .All()
                .Where(c => c.UserId.Equals(userId) && c.ComponentTypeId.Equals(typeId))
                .To<T>(new { language })
                .ToListAsync();

        public async Task<IEnumerable<T>> AllByUserIdAndTypeIdActiveLocalized<T>(string userId, int typeId, Languages language)
            => await this.componentsRepository
                .All()
                .Where(c => c.UserId.Equals(userId) && c.ComponentTypeId.Equals(typeId) && c.IsActive)
                .To<T>(new { language })
                .ToListAsync();

        public void Delete(Component component)
            => this.componentsRepository.Delete(component);

        public async Task<T> GetByIdAsync<T>(string userId, string id)
            => await this.componentsRepository
                .All()
                .Where(c => c.UserId.Equals(userId) && c.Id.Equals(id))
                .To<T>()
                .FirstOrDefaultAsync();

        public async Task<Component> GetByIdAsync(string userId, string id)
            => await this.componentsRepository.GetByIdWithoutDeletedAsync(userId, id);

        public async Task<bool> AddAmount(string userId, string componentId, decimal amount)
        {
            Component entityToUpdate = await this.componentsRepository.GetByIdWithoutDeletedAsync(userId, componentId);

            if (entityToUpdate == null)
            {
                return false;
            }

            entityToUpdate.Amount += amount;

            await this.componentsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveAmount(string userId, string componentId, decimal amount)
        {
            Component entityToUpdate = await this.componentsRepository.GetByIdWithoutDeletedAsync(userId, componentId);

            if (entityToUpdate == null)
            {
                return false;
            }

            entityToUpdate.Amount -= amount;

            await this.componentsRepository.SaveChangesAsync();

            return true;
        }

        public async Task AddAsync(Component component)
        {
            await this.componentsRepository.AddAsync(component);
            await this.componentsRepository.SaveChangesAsync();
        }

        public async Task<bool> TransactionBetweenComponents(Component senderComponent, Component receiverComponent, decimal transactionAmount)
        {
            if (senderComponent.Amount < transactionAmount || transactionAmount < 0)
            {
                return false;
            }

            receiverComponent.Amount += transactionAmount;
            senderComponent.Amount -= transactionAmount;

            await this.componentsRepository.SaveChangesAsync();

            return true;
        }

        public bool CheckIfComponentIsValid(string userId, string componentId)
        => this.componentsRepository
                .AllAsNoTracking()
                .Any(x => x.Id.Equals(componentId) && x.UserId.Equals(userId));

        public bool CheckIfComponentHasEnoughAmount(string userId, string componentId, decimal amount)
            =>  this.componentsRepository.GetByIdWithoutDeletedAsync(userId, componentId).Result.Amount > amount;

        public async Task Update(string userId, Component component)
        {
            this.componentsRepository.Update(component);
            await this.componentsRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateComponentAmount(string userId, string componentId, decimal amountDifference, ComponentAmountUpdateTypes updateType)
        {
            Component entityToUpdate = await this.componentsRepository.GetByIdWithoutDeletedAsync(userId, componentId);

            if (entityToUpdate == null)
            {
                return false;
            }

            switch (updateType)
            {
                case ComponentAmountUpdateTypes.Income:
                    entityToUpdate.Amount += amountDifference;
                    break;
                case ComponentAmountUpdateTypes.Expense:
                    entityToUpdate.Amount -= amountDifference;
                    break;
                default:
                    break;
            }

            this.componentsRepository.Update(entityToUpdate);
            await this.componentsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<decimal> AmountSumOfActiveComponentsByTypeAndUserId(string userId, ComponentTypes componentType)
            => await this.componentsRepository
                .All()
                .Where(c => c.UserId.Equals(userId) && c.ComponentTypeId.Equals((int)componentType) && c.IsActive)
                .Select(c => c.Amount)
                .SumAsync();      
    }
}
