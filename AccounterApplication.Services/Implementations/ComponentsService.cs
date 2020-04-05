namespace AccounterApplication.Services.Implementations
{
    using System;
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

        public void Delete(Component component)
            => this.componentsRepository.Delete(component);

        public async Task<Component> GetByIdAsync(string userId, string id)
            => await this.componentsRepository.GetByIdWithoutDeletedAsync(userId, id);

        public async Task<bool> AddAmount(string userId, Component component)
        {
            Component entityToUpdate = await this.componentsRepository.GetByIdWithoutDeletedAsync(userId, component.Id);

            if (entityToUpdate == null)
            {
                return false;
            }

            entityToUpdate.Amount += component.Amount;

            await this.componentsRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveAmount(string userId, Component component)
        {
            Component entityToUpdate = await this.componentsRepository.GetByIdWithoutDeletedAsync(userId, component.Id);

            if (entityToUpdate == null)
            {
                return false;
            }

            entityToUpdate.Amount -= component.Amount;

            await this.componentsRepository.SaveChangesAsync();

            return true;
        }
    }
}
