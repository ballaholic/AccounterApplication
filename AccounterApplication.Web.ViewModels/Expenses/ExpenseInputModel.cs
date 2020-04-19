namespace AccounterApplication.Web.ViewModels.Expenses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AutoMapper;

    using Components;
    using Data.Models;
    using ExpenseGroups;
    using Services.Mapping;

    using Resources = Common.LocalizationResources.ViewModels.ExpenseInputModelResources;

    public class ExpenseInputModel : IMapFrom<Expense>, IMapTo<Expense>, IHaveCustomMappings
    {
        public ExpenseInputModel()
        {
            this.ExpenseAmount = 0.1m;
            this.ExpenseDate = DateTime.UtcNow.Date;
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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "DateNotValid")]
        public DateTime ExpenseDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "ExpenseGroup", ResourceType = typeof(Resources))]
        public int ExpenseGroupId { get; set; }

        public IEnumerable<ExpenseGroupSelectListItem> ExpenseGroupListItems { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Component", ResourceType = typeof(Resources))]
        public string ComponentId { get; set; }

        [Display(Name = "Component", ResourceType = typeof(Resources))]
        public string ComponentName { get; set; }

        public IEnumerable<ComponentsSelectListItem> ComponentsSelectListItems { get; set; }


        public void CreateMappings(IProfileExpression configuration)
           => configuration.CreateMap<Expense, ExpenseInputModel>().ForMember(
               m => m.ComponentName,
               opt => opt.MapFrom(x => $"{x.Component.Name} - {x.Component.Currency.Code}"));
    }
}
