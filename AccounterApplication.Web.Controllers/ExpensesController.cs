namespace AccounterApplication.Web.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Infrastructure;
    using Services.Contracts;
    using ViewModels.Expenses;
    using AccounterApplication.Data.Models;

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

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddExpense(ExpenseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var entityToAdd = new Expense
            {
                UserId = this.User.GetLoggedInUserId<string>(),
                Description = model.Description,
                ExpenseAmount = model.ExpenseAmount,
                CreatedOn = model.DateOfExpense,
                IsDeleted = false,
                DeletedOn = null
            };

            await this.expensesService.AddAsync(entityToAdd);

            return this.RedirectToAction("Index");
        }
    }
}
