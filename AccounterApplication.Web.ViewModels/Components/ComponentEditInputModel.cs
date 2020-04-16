namespace AccounterApplication.Web.ViewModels.Components
{
    using Microsoft.AspNetCore.Mvc.ModelBinding;

    using Data.Models;
    using Services.Mapping;

    public class ComponentEditInputModel : IMapFrom<Component>, IMapTo<Component>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}
