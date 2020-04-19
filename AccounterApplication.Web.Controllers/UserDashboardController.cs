namespace AccounterApplication.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;

    using Services.Contracts;
    using Common.Enumerations;
    using ViewModels.Expenses;
    using ViewModels.UserDashboard;

    public class UserDashboardController : BaseController
    {
        private readonly IExpenseService expenseService;
        private readonly IComponentsService componentsService;

        public UserDashboardController(IExpenseService expenseService, IComponentsService componentsService)
        {
            this.expenseService = expenseService;
            this.componentsService = componentsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId<string>();
            var language = this.GetCurrentLanguage();
            var countOfExpenses = 5;
            var lastExpenses = await this.expenseService.NewestByUserIdLocalized<ExpenseViewModel>(userId, language, countOfExpenses);
            var fundsSum = await this.componentsService.AmountSumOfActiveComponentsByTypeAndUserId(userId, ComponentTypes.PaymentComponent);
            var savingsSum = await this.componentsService.AmountSumOfActiveComponentsByTypeAndUserId(userId, ComponentTypes.SavingsComponent);

            var viewModel = new UserDashboardViewModel
            {
                FundsAmount = fundsSum, 
                SavingsAmount = savingsSum,
                Expenses = lastExpenses
            };

            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetLastExpensesByCount(int countOfExpenses)
        {
            var userId = this.GetUserId<string>();
            var language = this.GetCurrentLanguage();
            var viewModel = await this.expenseService.NewestByUserIdLocalized<ExpenseViewModel>(userId, language, countOfExpenses);

            return PartialView("_UserDashboardLastExpensesPartial", viewModel);
        }
    }
}
