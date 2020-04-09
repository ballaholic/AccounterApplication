namespace AccounterApplication.Web.ViewModels.Components
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using AccounterApplication.Web.ViewModels.ComponentTypes;
    using AccounterApplication.Web.ViewModels.Currencies;
    using Data.Models;
    using Services.Mapping;

    using Resources = Common.LocalizationResources.ViewModels.ComponentInputModelResources;

    public class ComponentInputModel : IMapTo<Component>
    {
        public ComponentInputModel()
        {
            this.Amount = 0m;
            this.IsActive = true;
        }

        [Required]
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Name", ResourceType = typeof(Resources))]
        [MaxLength(50, ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "NameTooLong")]
        public string Name { get; set; }

        [Required]
        [Range(0, 0)]
        public decimal Amount { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "IsActive", ResourceType = typeof(Resources))]
        public bool IsActive { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "Currency", ResourceType = typeof(Resources))]
        public int CurrencyId { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources), ErrorMessageResourceName = "Required")]
        [Display(Name = "ComponentType", ResourceType = typeof(Resources))]
        public int ComponentTypeId { get; set; }

        public IEnumerable<CurrencySelectListItem> CurrencyListItems { get; set; }

        public IEnumerable<ComponentTypeSelectListItem> ComponentTypeListItems { get; set; }
    }
}
