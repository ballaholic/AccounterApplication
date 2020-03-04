namespace AccounterApplication.Web.ViewModels.MonthlyIncomes
{
    using System.Collections.Generic;

    public class MonthlyIncomesListingViewModel
    {
        public IEnumerable<MonthlyIncomeViewModel> MonthlyIncomes { get; set; }
    }
}
