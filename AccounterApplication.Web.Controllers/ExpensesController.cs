namespace AccounterApplication.Web.Controllers
{
    using AccounterApplication.Web.Controllers.Infrastructure;
    using AccounterApplication.Web.ViewModels.Expenses;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Services.Contracts;
    using System.Threading.Tasks;

    public class ExpensesController : BaseController
    {
        private readonly IExpenseService expensesService;

        public ExpensesController(IExpenseService expenses)
            => this.expensesService = expenses;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.GetLoggedInUserId<string>();
            var expenses = await this.expensesService.AllByUserId<ExpenseViewModel>(userId);
            var viewModel = new ExpensesListingViewModel { Expenses = expenses };
            return View(viewModel);
        }


    }
}
