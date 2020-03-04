namespace AccounterApplication.Web.ViewModels.UserDashboard
{
    using System.Collections.Generic; 
    using MonthlyIncomes;

    public class UserDashboardViewModel
    {
        public IEnumerable<MonthlyIncomesListingViewModel> MonthlyIncomes { get; set; }
    }
}
