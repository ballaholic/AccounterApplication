namespace AccounterApplication.Services.Contracts
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Common.Enumerations;

    public interface ICurrenciesService
    {
        Task<IEnumerable<T>> All<T>();

        Task<IEnumerable<T>> AllLocalized<T>(Languages language);
    }
}
