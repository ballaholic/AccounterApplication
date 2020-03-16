namespace AccounterApplication.Web.ViewModels.Expenses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ExpenseInputModel
    {
        public ExpenseInputModel()
        {
            this.ExpenseAmount = 0.1m;
            this.DateOfExpense = DateTime.UtcNow.Date;
        }

        [Required]
        [MinLength(2)]
        [MaxLength(200)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Amount")]
        [Range(0.1, 1000000)]
        public decimal ExpenseAmount { get; set; }

        [Required]
        [Display(Name = "Date of Expense")]
        [DataType(DataType.Date)]
        public DateTime DateOfExpense { get; set; }
    }
}
