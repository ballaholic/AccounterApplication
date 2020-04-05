namespace AccounterApplication.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Models;
    using System.Linq;

    internal class ComponentTypesSeeder : ISeeder
    {
        public async Task SeedAsync(AccounterDbContext dbContext, IServiceProvider serviceProvider)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.ComponentTypes.Any())
            {
                return;
            }

            List<ComponentType> componentTypes = this.GenerateEntities();

            foreach (var item in componentTypes)
            {
                await dbContext.ComponentTypes.AddAsync(item);
            }
        }

        private List<ComponentType> GenerateEntities()
            => new List<ComponentType> 
            {
                new ComponentType
                {
                    NameEN = "Payment Component",
                    NameBG = "Разплащателна Компонента",
                    IsMain = true
                },
                new ComponentType
                {
                    NameEN = "Savings Component",
                    NameBG = "Спестовна Компонента",
                    IsMain = true
                }
            };
    }
}
