namespace AccounterApplication.Web.ViewModels.Expenses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;

    using Data.Models;
    using ExpenseGroups;
    using Services.Mapping;

    using Resources = Common.LocalizationResources.ViewModels.ExpenseInputModelResources;

    public class ExpenseInputModel : IMapTo<Expense>, IHaveCustomMappings
    {
        public ExpenseInputModel()
        {
            this.ExpenseAmount = 0.1m;
            this.DateOfExpense = DateTime.UtcNow.Date;
        }

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Description", ResourceType = typeof(Resources))]
        [MaxLength(200, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "DescriptionTooLong")]
        public string Description { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Amount", ResourceType = typeof(Resources))]
        [Range(0.01, 1000000000, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "ExpenseAmountNotInRange")]
        public decimal ExpenseAmount { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "ExpenseDate", ResourceType = typeof(Resources))]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "DateNotValid")]
        public DateTime DateOfExpense { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "ExpenseGroup", ResourceType = typeof(Resources))]
        public int ExpenseGroupId { get; set; }

        public IEnumerable<ExpenseGroupSelectListItem> ExpenseGroupListItems { get; set; }

        public void CreateMappings(IProfileExpression configuration)
           => configuration.CreateMap<ExpenseInputModel, Expense>().ForMember(
               m => m.ExpenseDate,
               opt => opt.MapFrom(x => x.DateOfExpense));
    }
}
