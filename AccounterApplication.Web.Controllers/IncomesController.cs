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
    using ViewModels.MonthlyIncomes;
    using AccounterApplication.Common.GlobalConstants;

    using AlertType = Common.Enumerations.AlertMessageTypes;
    using Resources = Common.LocalizationResources.Shared.Messages.MessagesResources;
    using AccounterApplication.Web.ViewModels.Components;

    public class IncomesController : BaseController
    {
        private readonly IComponentsService componentsService;
        private readonly IMonthlyIncomeService monthlyIncomeService;

        public IncomesController(IMonthlyIncomeService monthlyIncomeService, IComponentsService componentsService)
        {
            this.componentsService = componentsService;
            this.monthlyIncomeService = monthlyIncomeService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            var monthlyIncomes = await this.monthlyIncomeService.AllByUserIdLocalized<MonthlyIncomeViewModel>(userId, language);
            var viewModel = new MonthlyIncomesListingViewModel { MonthlyIncomes = monthlyIncomes };

            return this.View(viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSortedByDate(string sortType)
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            IEnumerable<MonthlyIncomeViewModel> incomes;

            if (Enum.TryParse(sortType, true, out SortTypes parsedType))
            {
                incomes = await this.monthlyIncomeService.AllByUserIdLocalizedSortedByDate<MonthlyIncomeViewModel>(userId, language, parsedType);
            }
            else
            {
                incomes = await this.monthlyIncomeService.AllByUserIdLocalized<MonthlyIncomeViewModel>(userId, language);
            }

            var viewModel = new MonthlyIncomesListingViewModel { MonthlyIncomes = incomes };

            return this.PartialView("_MonthlyIncomesTableBodyPartial", viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSortedByAmount(string sortType)
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            IEnumerable<MonthlyIncomeViewModel> incomes;

            if (Enum.TryParse(sortType, true, out SortTypes parsedType))
            {
                incomes = await this.monthlyIncomeService.AllByUserIdLocalizedSortedByAmount<MonthlyIncomeViewModel>(userId, language, parsedType);
            }
            else
            {
                incomes = await this.monthlyIncomeService.AllByUserIdLocalized<MonthlyIncomeViewModel>(userId, language);
            }

            var viewModel = new MonthlyIncomesListingViewModel { MonthlyIncomes = incomes };

            return this.PartialView("_MonthlyIncomesTableBodyPartial", viewModel);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddMonthlyIncome()
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            var componentTypeId = (int)ComponentTypes.PaymentComponent;
            var model = new MonthlyIncomeInputModel { ComponentsSelectListItems = await this.componentsService.AllByUserIdAndTypeIdActiveLocalized<ComponentsSelectListItem>(userId, componentTypeId, language) };

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMonthlyIncome(MonthlyIncomeInputModel model)
        {
            var userId = this.GetUserId<string>();
            var language = this.GetCurrentLanguage();
            var componentTypeId = (int)ComponentTypes.PaymentComponent;

            if (!this.ModelState.IsValid)
            {
                model.ComponentsSelectListItems = await this.componentsService.AllByUserIdAndTypeIdActiveLocalized<ComponentsSelectListItem>(userId, componentTypeId, language);
                return this.View(model);
            }

            try
            {
                var entityToAdd = new MonthlyIncome
                {
                    Amount = model.Amount,
                    UserId = userId,
                    IncomePeriod = model.IncomePeriod,
                    ComponentId = model.ComponentId
                };

                await this.monthlyIncomeService.AddAsync(entityToAdd);

                var result = await this.componentsService.AddAmount(userId, model.ComponentId, model.Amount);

                if (result)
                {
                    this.AddAlertMessageToTempData(AlertType.Success, Resources.Success, Resources.MonthlyIncomeAddSuccess);
                }
                else
                {
                    this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.MonthlyIncomeAddError);
                }
            }
            catch (Exception)
            {
                this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.MonthlyIncomeAddError);
            }

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditMonthlyIncome(int id)
        {
            var userId = this.GetUserId<string>();
            var model = await this.monthlyIncomeService.GetByIdAsync<MonthlyIncomeInputModel>(userId, id);

            return this.View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditMonthlyIncome(MonthlyIncomeInputModel model, string submitType)
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

                var monthlyIncome = await this.monthlyIncomeService.GetByIdAsync(userId, model.Id);
                var amountDifference = model.Amount - monthlyIncome.Amount;

                this.Mapper.Map(model, monthlyIncome);

                await this.monthlyIncomeService.Update(userId, monthlyIncome);
                await this.componentsService.UpdateComponentAmount(userId, model.ComponentId, amountDifference, ComponentAmountUpdateTypes.Income);

                this.AddAlertMessageToTempData(AlertType.Success, Resources.Success, Resources.MonthlyIncomeUpdatedSuccess);
            }
            catch (Exception)
            {
                this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.MonthlyIncomeUpdatedError);
            }

            return this.RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteMonthlyIncome(int id)
        {
            var userId = this.GetUserId<string>();
            bool itemIdIsValid = this.monthlyIncomeService.CheckIfMonthlyIncomeIdIsValid(id, userId);
            bool result;


            if (itemIdIsValid)
            {
                var monthlyIncome = await this.monthlyIncomeService.GetByIdAsync(userId, id);
                this.monthlyIncomeService.Delete(monthlyIncome);

                this.AddAlertMessageToTempData(AlertType.Success, Resources.Success, Resources.MonthlyIncomeDeleteSuccess);
                result = true;
            }
            else
            {
                this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.MonthlyIncomeDeleteError);
                result = false;
            }

            return this.Json(result);
        }
    }
}
