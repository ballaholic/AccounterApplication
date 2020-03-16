namespace AccounterApplication.Web.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Infrastructure;
    using Services.Contracts;
    using AccounterApplication.Web.ViewModels.MonthlyIncomes;
    using AccounterApplication.Data.Models;

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
            return View();
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
    }
}
