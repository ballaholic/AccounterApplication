namespace AccounterApplication.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Models;
    using System.Linq;

    internal class CurrenciesSeeder : ISeeder
    {
        public async Task SeedAsync(AccounterDbContext dbContext, IServiceProvider serviceProvider)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.Currencies.Any())
            {
                return;
            }

            List<Currency> currencies = this.GenerateEntities();

            foreach (var item in currencies)
            {
                await dbContext.Currencies.AddAsync(item);
            }
        }

        private List<Currency> GenerateEntities()
            => new List<Currency> 
            {
                new Currency
                {
                    NameEN = "Bulgarian Lev",
                    NameBG = "Български Лев",
                    IsMain = true,
                    Sign = "лв.",
                    Code = "BGN"
                },
                new Currency
                {
                    NameEN = "United States dollar",
                    NameBG = "Щатски долар",
                    IsMain = true,
                    Sign = "$",
                    Code = "USD"
                },
                new Currency
                {
                    NameEN = "Euro",
                    NameBG = "Евро",
                    IsMain = true,
                    Sign = "€",
                    Code = "EUR"
                }
            };
    }
}
