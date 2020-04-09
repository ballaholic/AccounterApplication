namespace AccounterApplication.Web.ViewModels.Currencies
{
    using AutoMapper;

    using Data.Models;
    using Services.Mapping;
    using Common.Enumerations;

    using Microsoft.AspNetCore.Mvc.Rendering;

    public class CurrencySelectListItem : SelectListItem, IMapFrom<Currency>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
        {
            var language = Languages.Bulgarian;

            configuration.CreateMap<Currency, CurrencySelectListItem>()
                .ForMember(
                    m => m.Value,
                    opt => opt.MapFrom(x => x.Id))
                .ForMember(
                    m => m.Text,
                    opt => opt.MapFrom(x =>
                        language.Equals(Languages.Bulgarian)
                            ? x.NameBG
                            : x.NameEN));
        }
    }
}
