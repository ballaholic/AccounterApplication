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
    using ViewModels.Components;
    using ViewModels.ExpenseGroups;
    using AccounterApplication.Common.GlobalConstants;

    using AlertType = Common.Enumerations.AlertMessageTypes;
    using Resources = Common.LocalizationResources.Shared.Messages.MessagesResources;

    public class ExpensesController : BaseController
    {
        private readonly IExpenseService expensesService;
        private readonly IExpenseGroupService expenseGroupService;
        private readonly IComponentsService componentsService;

        public ExpensesController(IExpenseService expenses, IExpenseGroupService expenseGroups, IComponentsService componentsService)
        {
            this.expensesService = expenses;
            this.expenseGroupService = expenseGroups;
            this.componentsService = componentsService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            var expenses = await this.expensesService.AllByUserIdLocalized<ExpenseViewModel>(userId, language);
            var expenseGroups = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language);
            var viewModel = new ExpensesListingViewModel 
            { 
                Expenses = expenses,
                ExpenseGroupListItems = expenseGroups
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
            var userId = this.GetUserId<string>();
            var language = this.GetCurrentLanguage();
            var componentTypeId = (int)ComponentTypes.PaymentComponent;

            var model = new ExpenseInputModel
            {
                ExpenseGroupListItems = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language),
                ComponentsSelectListItems = await this.componentsService.AllByUserIdAndTypeIdActiveLocalized<ComponentsSelectListItem>(userId, componentTypeId, language)
            };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddExpense(ExpenseInputModel model)
        {
            var userId = this.GetUserId<string>();

            if (!this.ModelState.IsValid)
            {
                var language = this.GetCurrentLanguage();
                var componentTypeId = (int)ComponentTypes.PaymentComponent;

                model.ExpenseGroupListItems = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language);
                model.ComponentsSelectListItems = await this.componentsService.AllByUserIdAndTypeIdActiveLocalized<ComponentsSelectListItem>(userId, componentTypeId, language);

                return this.View(model);
            }

            if (!this.componentsService.CheckIfComponentHasEnoughAmount(userId, model.ComponentId, model.ExpenseAmount))
            {
                this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.NotEnoughAmount);

                var language = this.GetCurrentLanguage();
                var componentTypeId = (int)ComponentTypes.PaymentComponent;

                model.ExpenseGroupListItems = await this.expenseGroupService.AllLocalized<ExpenseGroupSelectListItem>(language);
                model.ComponentsSelectListItems = await this.componentsService.AllByUserIdAndTypeIdActiveLocalized<ComponentsSelectListItem>(userId, componentTypeId, language);

                return this.View(model);
            }

            try
            {
                var entityToAdd = new Expense
                {
                    UserId = userId,
                    ExpenseGroupId = model.ExpenseGroupId,
                    Description = model.Description,
                    ExpenseAmount = model.ExpenseAmount,
                    ExpenseDate = model.ExpenseDate,
                    ComponentId = model.ComponentId
                };

                await this.expensesService.AddAsync(entityToAdd);
                bool isComponentUpdated = await this.componentsService.RemoveAmount(userId, model.ComponentId, model.ExpenseAmount);

                if (isComponentUpdated)
                {
                    this.AddAlertMessageToTempData(AlertType.Success, Resources.Success, Resources.ExpenseAddSuccess);
                }
                else
                {
                    this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.ExpenseAddError);
                }
            }
            catch (Exception)
            {
                this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.ExpenseAddError);
            }

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditExpense(int id)
        {
            var userId = this.GetUserId<string>();
            var expense = await this.expensesService.GetByIdAsync(userId, id);
            var model = await this.expensesService.GetByIdAsync<ExpenseInputModel>(userId, id);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditExpense(ExpenseInputModel model, string submitType)
        {
            if (submitType.Equals(ButtonValueConstants.ButtonCancel))
            {
                return this.RedirectToAction("Index");
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            try
            {
                var userId = this.GetUserId<string>();

                var expense = await this.expensesService.GetByIdAsync(userId, model.Id);
                var amountDifference = model.ExpenseAmount - expense.ExpenseAmount;

                this.Mapper.Map(model, expense);

                bool isExpenseUpdated = await this.expensesService.Update(userId, expense);

                bool isComponentUpdated = await this.componentsService.UpdateComponentAmount(userId, model.ComponentId, amountDifference, ComponentAmountUpdateTypes.Expense);

                if (isExpenseUpdated && isComponentUpdated)
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
