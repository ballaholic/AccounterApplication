namespace AccounterApplication.Web.ViewModels.MonthlyIncomes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MonthlyIncomeInputModel
    {
        public MonthlyIncomeInputModel()
        {
            this.Amount = 0.1m;
            this.IncomePeriod = DateTime.UtcNow.Date;
        }

        [Required]
        [Range(0.1, 1000000)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Period of Income")]
        [DataType(DataType.Date)]
        public DateTime IncomePeriod { get; set; }
    }
}
