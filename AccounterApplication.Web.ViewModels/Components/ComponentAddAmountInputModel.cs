namespace AccounterApplication.Web.ViewModels.Components
{
    using System.ComponentModel.DataAnnotations;

    using Resources = Common.LocalizationResources.ViewModels.ComponentAddAmountInputModel;

    public class ComponentAddAmountInputModel
    {
        [Display(Name = "PaymentComponent", ResourceType = typeof(Resources))]
        public ComponentViewModel Component { get; set; }

        [Display(Name = "Amount", ResourceType = typeof(Resources))]
        [Range(0.1, 1000000000, ErrorMessageResourceName = "AmountNotInRange", ErrorMessageResourceType = typeof(Resources))]
        public decimal Amount { get; set; }
    }
}
