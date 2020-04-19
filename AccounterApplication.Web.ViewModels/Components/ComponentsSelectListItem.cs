namespace AccounterApplication.Web.ViewModels.Components
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    using AutoMapper;

    using Data.Models;
    using Services.Mapping;

    public class ComponentsSelectListItem : SelectListItem, IMapFrom<Component>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<Component, ComponentsSelectListItem>()
                .ForMember(
                    x => x.Value,
                    opt => opt.MapFrom(x => x.Id))
                .ForMember(
                    x => x.Text,
                    opt => opt.MapFrom(x => $"{x.Name} - ({x.Amount} - {x.Currency.Code})"));
    }
}

