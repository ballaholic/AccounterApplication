namespace AccounterApplication.Web.ViewModels.Expenses
{
    using System.Collections.Generic;

    public class ExpensesListingViewModel
    {
        public IEnumerable<ExpenseViewModel> Expenses { get; set; }
    }
}
