namespace AccounterApplication.Services.Models.Expenses
{
    public class ExpenseListingServiceModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Amount { get; set; }

        public string UserName { get; set; }
    }
}
