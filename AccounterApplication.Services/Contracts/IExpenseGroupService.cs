namespace AccounterApplication.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Common.Enumerations;

    public interface IExpenseGroupService
    {
        Task<IEnumerable<T>> All<T>();

        Task<IEnumerable<T>> AllLocalized<T>(Languages language);
    }
}
