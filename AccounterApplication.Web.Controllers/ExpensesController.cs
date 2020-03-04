namespace AccounterApplication.Web.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Infrastructure;
    using Services.Contracts;
    using ViewModels.Expenses;

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

        [HttpGet]
        [Authorize]
        public IActionResult AddExpense()
        {
            return View();
        }
    }
}
