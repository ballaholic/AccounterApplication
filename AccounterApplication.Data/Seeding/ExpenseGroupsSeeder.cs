namespace AccounterApplication.Data.Seeding
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Models;
    using System.Linq;

    internal class ExpenseGroupsSeeder : ISeeder
    {
        public async Task SeedAsync(AccounterDbContext dbContext, IServiceProvider serviceProvider)
        {
            dbContext.Database.EnsureCreated();

            if (dbContext.ExpenseGroups.Any())
            {
                return;
            }

            List<ExpenseGroup> expenseGroups = this.GenerateEntities();

            foreach (var item in expenseGroups)
            {
                await dbContext.ExpenseGroups.AddAsync(item);
            }
        }

        private List<ExpenseGroup> GenerateEntities()
        {
            Dictionary<string, string> expenseGroupNames = new Dictionary<string, string>
            {
                { "Битови сметки", "Household bills" },
                { "Жилище", "Housing" },
                { "Храна и консумативи", "Food and supplies" },
                { "Заеми", "Loans" },
                { "Транспорт", "Transportation" },
                { "Автомобил", "Car" },
                { "Деца", "Children" },
                { "Дрехи и обувки", "Clothes and shoes" },
                { "Лични", "Personal" },
                { "Цигари и алкохол", "Cigarettes and alcohol" },
                { "Развлечения", "Entertainment" },
                { "Хранене навън", "Eating out" },
                { "Образование", "Education" },
                { "Подаръци", "Gifts" },
                { "Спорт/Хоби", "Sports / Hobbies" },
                { "Пътуване/Отдих", "Travel / Leisure" },
                { "Медицински", "Medical" },
                { "Домашни любимци", "Pets" },
                { "Разни", "Miscellaneous" }
            };

            List<ExpenseGroup> expenseGroups = new List<ExpenseGroup>();

            foreach (var keyValuePair in expenseGroupNames)
            {
                ExpenseGroup expenseGroup = new ExpenseGroup()
                {
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = null,
                    IsDeleted = false,
                    DeletedOn = null,
                    NameEN = keyValuePair.Value,
                    NameBG = keyValuePair.Key,
                    IsMain = true
                };

                expenseGroups.Add(expenseGroup);
            }

            return expenseGroups;
        }
    }
}
