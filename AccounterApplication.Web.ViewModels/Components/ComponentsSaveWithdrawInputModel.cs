namespace AccounterApplication.Web.ViewModels.Components
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Resources = Common.LocalizationResources.ViewModels.ComponentSaveWithdrawInputModelResources;

    public class ComponentsSaveWithdrawInputModel
    {
        public string TargetComponentId { get; set; }

        public string TargetComponentName { get; set; }

        [Display(Name = "UserSavingsComponent", ResourceType = typeof(Resources))]
        public decimal TargetComponentAmount { get; set; }

        public string TargetComponentCurrencyCode { get; set; }

        [Display(Name = "UserPaymentComponent", ResourceType = typeof(Resources))]
        public string UserPaymentComponentId { get; set; }

        public IEnumerable<ComponentsSelectListItem> UserPaymentComponents { get; set; }

        [Display(Name = "TransactionAmount", ResourceType = typeof(Resources))]
        [Range(0.1, 1000000, ErrorMessageResourceName = "AmountNotInRange", ErrorMessageResourceType = typeof(Resources))]
        public decimal Amount { get; set; }
    }
}
