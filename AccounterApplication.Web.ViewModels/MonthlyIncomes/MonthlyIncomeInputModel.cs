namespace AccounterApplication.Web.ViewModels.MonthlyIncomes
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using Data.Models;
    using Services.Mapping;

    public class MonthlyIncomeInputModel : IMapTo<MonthlyIncome>
    {
        public MonthlyIncomeInputModel()
        {
            this.Amount = 0.1m;
            this.IncomePeriod = DateTime.UtcNow.Date;
        }

        [Required]
        public int Id { get; set; }

        [Required]
        [Range(0.1, 1000000)]
        public decimal Amount { get; set; }

        [Required]
        [Display(Name = "Period of Income")]
        [DataType(DataType.Date)]
        public DateTime IncomePeriod { get; set; }
    }
}
