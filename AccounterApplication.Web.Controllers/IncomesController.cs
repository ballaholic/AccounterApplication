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

    using AlertType = Common.Enumerations.AlertMessageTypes;
    using Resources = Common.LocalizationResources.Shared.Messages.MessagesResources;

    public class IncomesController : BaseController
    {
        private readonly IMonthlyIncomeService monthlyIncomeService;

        public IncomesController(IMonthlyIncomeService monthlyIncomeService)
            => this.monthlyIncomeService = monthlyIncomeService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var language = this.GetCurrentLanguage();
            var userId = this.GetUserId<string>();
            var monthlyIncomes = await this.monthlyIncomeService.AllByUserIdLocalized<MonthlyIncomeViewModel>(userId, language);
            var viewModel = new MonthlyIncomesListingViewModel { MonthlyIncomes = monthlyIncomes };
            return View(viewModel);
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

            return PartialView("_MonthlyIncomesTableBodyPartial", viewModel);
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

            return PartialView("_MonthlyIncomesTableBodyPartial", viewModel);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddMonthlyIncome()
        {
            var model = new MonthlyIncomeInputModel();
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddMonthlyIncome(MonthlyIncomeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            var entityToAdd = new MonthlyIncome
            {
                Amount = model.Amount,
                UserId = this.User.GetLoggedInUserId<string>(),
                IncomePeriod = model.IncomePeriod
            };

            await this.monthlyIncomeService.AddAsync(entityToAdd);

            this.AddAlertMessageToTempData(AlertType.Success, Resources.Success, Resources.MonthlyIncomeAddSuccess);

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditMonthlyIncome(int id)
        {
            var userId = this.GetUserId<string>();
            var monthlyIncome = await this.monthlyIncomeService.GetByIdAsync(userId, id);
            var model = new MonthlyIncomeInputModel
            {
                Id = monthlyIncome.Id,
                Amount = monthlyIncome.Amount,
                IncomePeriod = monthlyIncome.IncomePeriod
            };
            
            return View(model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditMonthlyIncome(MonthlyIncomeInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = this.GetUserId<string>();

                var monthlyIncome = await this.monthlyIncomeService.GetByIdAsync(userId, model.Id);

                this.Mapper.Map(model, monthlyIncome);

                bool isUpdated = await this.monthlyIncomeService.Update(userId, monthlyIncome);

                if (isUpdated)
                {
                    this.AddAlertMessageToTempData(AlertType.Success, Resources.Success, Resources.MonthlyIncomeUpdatedSuccess);
                }
                else
                {
                    this.AddAlertMessageToTempData(AlertType.Error, Resources.Error, Resources.MonthlyIncomeUpdatedError);   
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
