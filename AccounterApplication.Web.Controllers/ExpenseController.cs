namespace AccounterApplication.Web.Controllers
{
    using AccounterApplication.Data.Common.Repositories;
    using AccounterApplication.Data.Models;

    public class ExpenseController
    {
        private readonly IDeletableEntityRepository<Expense> repository;
    }
}
