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

    public class ComponentTypesService : IComponentTypesService
    {
        private readonly IDeletableEntityRepository<ComponentType> componentTypesRepository;

        public ComponentTypesService(IDeletableEntityRepository<ComponentType> componentTypesRepository) => this.componentTypesRepository = componentTypesRepository;

        public async Task<IEnumerable<T>> All<T>()
            => await this.componentTypesRepository
                .All()
                .To<T>()
                .ToListAsync();

        public async Task<IEnumerable<T>> AllLocalized<T>(Languages language)
            => await this.componentTypesRepository
                .All()
                .To<T>(new { language })
                .ToListAsync();
    }
}
