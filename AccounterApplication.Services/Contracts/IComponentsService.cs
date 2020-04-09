namespace AccounterApplication.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Common.Enumerations;
    using Data.Models;

    public interface IComponentsService
    {
        Task<IEnumerable<T>> AllByUserId<T>(string userId);

        Task<IEnumerable<T>> AllByUserIdLocalized<T>(string userId, Languages language);

        Task<Component> GetByIdAsync(string userId, string id);

        Task<bool> AddAmount(string userId, Component component);

        Task<bool> RemoveAmount(string userId, Component component);

        Task AddAsync(Component component);

        void Delete(Component component);
    }
}
