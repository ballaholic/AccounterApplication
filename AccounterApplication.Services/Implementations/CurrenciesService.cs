namespace AccounterApplication.Services.Implementations
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Microsoft.EntityFrameworkCore;

    using Mapping;
    using Contracts;
    using Data.Models;
    using Common.Enumerations;
    using Data.Common.Repositories;    

    public class CurrenciesService : ICurrenciesService
    {
        private readonly IDeletableEntityRepository<Currency> currencyRepository;

        public CurrenciesService(IDeletableEntityRepository<Currency> currencyRepository) => this.currencyRepository = currencyRepository;

        public async Task<IEnumerable<T>> All<T>()
            => await this.currencyRepository
                .All()
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllLocalized<T>(Languages language)
            => await this.currencyRepository
                .All()
                .To<T>(new { language })
                .ToListAsync();
    }
}
