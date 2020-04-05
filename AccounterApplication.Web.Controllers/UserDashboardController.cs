﻿namespace AccounterApplication.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;

    using System.Threading.Tasks;

    using Infrastructure;
    using Services.Contracts;
    using ViewModels.UserDashboard;
    using ViewModels.MonthlyIncomes;

    public class UserDashboardController : BaseController
    {
        private readonly IMonthlyIncomeService monthlyIncomeService;

        public UserDashboardController(IMonthlyIncomeService monthlyIncomeService)
            => this.monthlyIncomeService = monthlyIncomeService;

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = this.GetUserId<string>();
            var monthlyIncomes = await this.monthlyIncomeService.AllFromCurrentMonthByUserId<MonthlyIncomeViewModel>(userId);
            var viewModel = new UserDashboardViewModel { MonthlyIncomes = monthlyIncomes };

            return View(viewModel);
        }
    }
}
