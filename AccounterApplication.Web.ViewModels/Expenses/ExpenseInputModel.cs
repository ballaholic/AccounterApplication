namespace AccounterApplication.Web.ViewModels.Expenses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Data.Models;
    using Services.Mapping;

    public class ExpenseInputModel : IMapTo<Expense>
    {
        public ExpenseInputModel()
        {
            this.ExpenseAmount = 0.1m;
            this.DateOfExpense = DateTime.UtcNow.Date;
        }

        [Required]
        public int Id { get; set; }

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

        public void CreateMappings(IProfileExpression configuration)
           => configuration.CreateMap<ExpenseInputModel, Expense>().ForMember(
               m => m.CreatedOn,
               opt => opt.MapFrom(x => x.DateOfExpense));
    }
}
