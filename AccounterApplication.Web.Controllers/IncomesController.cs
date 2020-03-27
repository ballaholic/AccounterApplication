namespace AccounterApplication.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Data.Models;
    using Infrastructure;
    using Services.Contracts;
    using AlertType = Common.Enumerations.AlertMessageTypes;
    using Web.ViewModels.MonthlyIncomes;


    public class IncomesController : BaseController
    {
        private readonly IMonthlyIncomeService monthlyIncomeService;

        public IncomesController(IMonthlyIncomeService monthlyIncomeService)
            => this.monthlyIncomeService = monthlyIncomeService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = this.User.GetLoggedInUserId<string>();
            var monthlyIncomes = await this.monthlyIncomeService.AllFromCurrentMonthByUserId<MonthlyIncomeViewModel>(userId);
            var viewModel = new MonthlyIncomesListingViewModel { MonthlyIncomes = monthlyIncomes };
            return View(viewModel);
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

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditMonthlyIncome(int id)
        {
            var userId = this.User.GetLoggedInUserId<string>();
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
                var userId = this.User.GetLoggedInUserId<string>();

                var monthlyIncome = await this.monthlyIncomeService.GetByIdAsync(userId, model.Id);

                this.Mapper.Map(model, monthlyIncome);

                bool isUpdated = await this.monthlyIncomeService.Update(userId, monthlyIncome);

                if (isUpdated)
                {
                    this.AddAlertMessageToTempData(AlertType.Success, "Monthly Income is updated");
                }
                else
                {
                    this.AddAlertMessageToTempData(AlertType.Error, "There was an error trying to update Monthly Income");   
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
            var userId = this.User.GetLoggedInUserId<string>();
            bool itemIdIsValid = this.monthlyIncomeService.CheckIfMonthlyIncomeIdIsValid(id, userId);
            bool result;


            if (itemIdIsValid)
            {
                var monthlyIncome = await this.monthlyIncomeService.GetByIdAsync(userId, id);
                this.monthlyIncomeService.Delete(monthlyIncome);

                this.AddAlertMessageToTempData(AlertType.Success, "Monthly Income was deleted successfully");
                result = true;
            }
            else
            {
                this.AddAlertMessageToTempData(AlertType.Error, "There was an error trying to delete the Monthly Income");
                result = false;
            }

            return this.Json(result);
        }
    }
}
