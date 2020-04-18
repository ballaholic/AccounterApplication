namespace AccounterApplication.Web.ViewModels.UserDashboard
{
    using System.Collections.Generic;
    using AccounterApplication.Web.ViewModels.Expenses;
    using MonthlyIncomes;

    public class UserDashboardViewModel
    {
        public decimal FundsAmount { get; set; }

        public decimal SavingsAmount { get; set; }

        public IEnumerable<ExpenseViewModel> Expenses { get; set; }
    }
}
