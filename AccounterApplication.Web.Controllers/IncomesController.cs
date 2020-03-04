namespace AccounterApplication.Web.Controllers
{
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using Infrastructure;
    using Services.Contracts;
    using AccounterApplication.Web.ViewModels.MonthlyIncomes;

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
    }
}
