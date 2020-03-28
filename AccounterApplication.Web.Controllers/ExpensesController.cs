namespace AccounterApplication.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data.Models;
    using Infrastructure;
    using Services.Contracts;
    using ViewModels.Expenses;

    using AlertType = Common.Enumerations.AlertMessageTypes;

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
            var model = new ExpenseInputModel();
            return View(model);
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

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditExpense(int id)
        {
            var userId = this.User.GetLoggedInUserId<string>();
            var expense = await this.expensesService.GetByIdAsync(userId, id);
            var model = new ExpenseInputModel
            {
                Id = expense.Id,
                DateOfExpense = expense.CreatedOn,
                ExpenseAmount = expense.ExpenseAmount,
                Description = expense.Description
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditExpense(ExpenseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = this.User.GetLoggedInUserId<string>();

                var expense = await this.expensesService.GetByIdAsync(userId, model.Id);

                this.Mapper.Map(model, expense);

                bool isUpdated = await this.expensesService.Update(userId, expense);

                if (isUpdated)
                {
                    this.AddAlertMessageToTempData(AlertType.Success, "The Expense was updated");
                }
                else
                {
                    this.AddAlertMessageToTempData(AlertType.Error, "There was an error trying to update the Expense");
                }

                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var userId = this.User.GetLoggedInUserId<string>();
            bool itemIdIsValid = this.expensesService.CheckIfExpenseIdIsValid(id, userId);
            bool result;

            if (itemIdIsValid)
            {
                var expense = await this.expensesService.GetByIdAsync(userId, id);
                this.expensesService.Delete(expense);

                this.AddAlertMessageToTempData(AlertType.Success, "The Expense was deleted successfully");
                result = true;
            }
            else
            {
                this.AddAlertMessageToTempData(AlertType.Error, "There was an error trying to delete the Expense");
                result = false;
            }

            return this.Json(result);
        }
    }
}
