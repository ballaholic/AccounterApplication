namespace AccounterApplication.Web.ViewModels.ComponentTypes
{
    using AutoMapper;

    using Data.Models;
    using Services.Mapping;
    using Common.Enumerations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ComponentTypeSelectListItem : SelectListItem, IMapFrom<ComponentType>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            var language = Languages.Bulgarian;

            configuration.CreateMap<ComponentType, ComponentTypeSelectListItem>()
                .ForMember(
                    m => m.Value,
                    opt => opt.MapFrom(x => x.Id))
                .ForMember(
                    m => m.Text,
                    opt => opt.MapFrom(x =>
                        language.Equals(Languages.English)
                            ? x.NameEN
                            : x.NameBG));
        }
    }
}
