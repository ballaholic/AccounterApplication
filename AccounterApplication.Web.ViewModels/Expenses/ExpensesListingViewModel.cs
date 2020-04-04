namespace AccounterApplication.Web.ViewModels.Expenses
{
    using System.Collections.Generic;

    using ExpenseGroups;

    public class ExpensesListingViewModel
    {
        public IEnumerable<ExpenseViewModel> Expenses { get; set; }

        public IEnumerable<ExpenseGroupSelectListItem> ExpenseGroupListItems { get; set; }
    }
}
