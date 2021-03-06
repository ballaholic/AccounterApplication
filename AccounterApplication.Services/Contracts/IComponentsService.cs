﻿namespace AccounterApplication.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Data.Models;
    using Common.Enumerations;

    public interface IComponentsService
    {
        Task<IEnumerable<T>> AllByUserId<T>(string userId);

        Task<IEnumerable<T>> AllByUserIdLocalized<T>(string userId, Languages language);

        Task<IEnumerable<T>> AllByUserIdActiveLocalized<T>(string userId, Languages language);

        Task<IEnumerable<T>> AllByUserIdAndTypeIdLocalized<T>(string userId, int typeId, Languages language);

        Task<IEnumerable<T>> AllByUserIdAndTypeIdActiveLocalized<T>(string userId, int typeId, Languages language);

        Task<T> GetByIdAsync<T>(string userId, string id);

        Task<Component> GetByIdAsync(string userId, string id);

        Task<bool> AddAmount(string userId, string componentId, decimal amount);

        Task<bool> RemoveAmount(string userId, string componentId, decimal amount);

        Task<bool> TransactionBetweenComponents(Component senderComponent, Component receiverComponent, decimal transactionAmount);

        Task AddAsync(Component component);

        void Delete(Component component);

        Task Update(string userId, Component component);

        Task<bool> UpdateComponentAmount(string userId, string componentId, decimal amountDifference, ComponentAmountUpdateTypes updateType);

        bool CheckIfComponentIsValid(string userId, string componentId);

        bool CheckIfComponentHasEnoughAmount(string userId, string componentId, decimal amount);

        Task<decimal> AmountSumOfActiveComponentsByTypeAndUserId(string userId, ComponentTypes componentType);
    }
}
