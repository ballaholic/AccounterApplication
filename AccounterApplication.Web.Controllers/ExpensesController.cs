namespace AccounterApplication.Web.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data.Models;
    using Infrastructure;
    using Services.Contracts;
    using Common.Enumerations;
    using ViewModels.Expenses;
    using ViewModels.ExpenseGroups;

    using AlertType = Common.Enumerations.AlertMessageTypes;
    using Resources = Common.LocalizationResources.Shared.Messages.MessagesResources;

    public class ExpensesController : BaseController
    {
        private readonly IExpenseService expensesService;
        private readonly IExpenseGroupService expenseGroupService;

        public ExpensesController(IExpenseService expenses, IExpenseGroupService expenseGroups)
        {
            this.expensesService = expenses;
            this.expenseGroupService = expenseGroups;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            var expenses = await this.expensesService.AllByUserIdLocalized<ExpenseViewModel>(userId, language);
            var viewModel = new ExpensesListingViewModel 
            { 
                Expenses = expenses,
                ExpenseGroupListItems = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language)
            };
            return View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSortedByDate(string sortType, int groupId)
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            IEnumerable<ExpenseViewModel> expenses;

            if (Enum.TryParse(sortType, true, out SortTypes parsedType))
            {
                expenses = await this.expensesService.AllByUserIdAndGroupIdLocalizedSortedByDate<ExpenseViewModel>(userId, groupId, language, parsedType);
            }
            else
            {
                expenses = await this.expensesService.AllByUserIdLocalized<ExpenseViewModel>(userId, language);
            }
          
            var viewModel = new ExpensesListingViewModel 
            { 
                Expenses = expenses,
                ExpenseGroupListItems = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language)
            };

            return PartialView("_ExpensesTableBodyPartial", viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSortedByAmount(string sortType, int groupId)
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            IEnumerable<ExpenseViewModel> expenses;

            if (Enum.TryParse(sortType, true, out SortTypes parsedType))
            {
                expenses = await this.expensesService.AllByUserIdAndGroupIdLocalizedSortedByAmount<ExpenseViewModel>(userId, groupId, language, parsedType);
            }
            else
            {
                expenses = await this.expensesService.AllByUserIdLocalized<ExpenseViewModel>(userId, language);
            }

            var viewModel = new ExpensesListingViewModel 
            {
                Expenses = expenses,
                ExpenseGroupListItems = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language)
            };

            return PartialView("_ExpensesTableBodyPartial", viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSortedByDescription(string sortType, int groupId)
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            IEnumerable<ExpenseViewModel> expenses;

            if (Enum.TryParse(sortType, true, out SortTypes parsedType))
            {
                expenses = await this.expensesService.AllByUserIdAndGroupIdLocalizedSortedByDescription<ExpenseViewModel>(userId, groupId, language, parsedType);
            }
            else
            {
                expenses = await this.expensesService.AllByUserIdLocalized<ExpenseViewModel>(userId, language);
            }

            var viewModel = new ExpensesListingViewModel 
            {
                Expenses = expenses,
                ExpenseGroupListItems = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language)
            };

            return PartialView("_ExpensesTableBodyPartial", viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetGrouped(int groupId)
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            IEnumerable<ExpenseViewModel> expenses;

            if (groupId == 0)
            {
                expenses = await this.expensesService.AllByUserIdLocalized<ExpenseViewModel>(userId, language);
            }
            else
            {
                expenses = await this.expensesService.AllByUserIdAndGroupIdLocalized<ExpenseViewModel>(userId, groupId, language);
            }
            
            var viewModel = new ExpensesListingViewModel
            {
                Expenses = expenses,
                ExpenseGroupListItems = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language)
            };

            return PartialView("_ExpensesTableBodyPartial", viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddExpense()
        {
            var language = this.GetCurrentLanguage();

            var model = new ExpenseInputModel
            {
                ExpenseGroupListItems = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language)
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddExpense(ExpenseInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                var language = this.GetCurrentLanguage();

                model.ExpenseGroupListItems = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language);

                return View(model);
            }

            var entityToAdd = new Expense
            {
                UserId = this.User.GetLoggedInUserId<string>(),
                ExpenseGroupId = model.ExpenseGroupId,
                Description = model.Description,
                ExpenseAmount = model.ExpenseAmount,
                ExpenseDate = model.DateOfExpense
            };

            await this.expensesService.AddAsync(entityToAdd);

            this.AddAlertMessageToTempData(AlertType.Success, Resources.Success, Resources.ExpenseAddSuccess);

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditExpense(int id)
        {
            var userId = this.GetUserId<string>();
            var expense = await this.expensesService.GetByIdAsync(userId, id);
            var model = new ExpenseInputModel
            {
                Id = expense.Id,
                ExpenseGroupId = expense.ExpenseGroupId,
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
                var userId = this.GetUserId<string>();

                var expense = await this.expensesService.GetByIdAsync(userId, model.Id);

                this.Mapper.Map(model, expense);

                bool isUpdated = await this.expensesService.Update(userId, expense);

                if (isUpdated)
                {
                    this.AddAlertMessageToTempData(AlertType.Success, Resources.Success, Resources.ExpenseUpdatedSuccess);
                }
                else
                {
                    this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.ExpenseUpdatedError);
                }

                return this.RedirectToAction("Index");
            }
            catch (Exception)
            {
                return this.View("Error");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var userId = this.GetUserId<string>();
            bool itemIdIsValid = this.expensesService.CheckIfExpenseIdIsValid(id, userId);
            bool result;

            if (itemIdIsValid)
            {
                var expense = await this.expensesService.GetByIdAsync(userId, id);
                this.expensesService.Delete(expense);

                this.AddAlertMessageToTempData(AlertType.Success, Resources.Success, Resources.ExpenseDeleteSuccess);
                result = true;
            }
            else
            {
                this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.ExpenseDeleteError);
                result = false;
            }

            return this.Json(result);
        }
    }
}
