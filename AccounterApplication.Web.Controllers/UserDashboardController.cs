namespace AccounterApplication.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;

    using Services.Contracts;
    using ViewModels.Expenses;
    using ViewModels.UserDashboard;
    using ViewModels.MonthlyIncomes;

    public class UserDashboardController : BaseController
    {
        private readonly IExpenseService expenseService;

        public UserDashboardController(IExpenseService expenseService)
            => this.expenseService = expenseService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId<string>();
            var language = this.GetCurrentLanguage();

            var lastTenExpenses = await this.expenseService.NewestByUserIdLocalized<ExpenseViewModel>(userId, language, 10);

            var viewModel = new UserDashboardViewModel
            {
                FundsAmount = 3000, 
                SavingsAmount = 2000,
                Expenses = lastTenExpenses
            };

            return View(viewModel);
        }
    }
}
