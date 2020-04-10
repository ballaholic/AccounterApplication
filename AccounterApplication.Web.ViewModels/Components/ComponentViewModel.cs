namespace AccounterApplication.Web.ViewModels.Components
{
    using AutoMapper;

    using Data.Models;
    using Services.Mapping;
    using Common.Enumerations;

    public class ComponentViewModel : IMapFrom<Component>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string ComponentNameAndCurrencyCode { get; set; }

        public decimal Amount { get; set; }

        public bool IsActive { get; set; }

        public int ComponentTypeId { get; set; }

        public string ComponentTypeName { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencyCode { get; set; }

        public string CurrencySign { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            var language = Languages.English;

            configuration.CreateMap<Component, ComponentViewModel>()
                .ForMember(
                    m => m.ComponentTypeName,
                    opt => opt.MapFrom(x =>
                        language.Equals(Languages.Bulgarian)
                            ? x.ComponentType.NameBG
                            : x.ComponentType.NameEN))
                .ForMember(
                    m => m.CurrencyName,
                    opt => opt.MapFrom(x =>
                        language.Equals(Languages.Bulgarian)
                            ? x.Currency.NameBG
                            : x.Currency.NameEN))
                .ForMember(
                    m => m.CurrencyCode,
                    opt => opt.MapFrom(x => x.Currency.Code))
                .ForMember(
                    m => m.CurrencySign,
                    opt => opt.MapFrom(x => x.Currency.Sign))
                .ForMember(
                    m => m.ComponentNameAndCurrencyCode,
                    opt => opt.MapFrom(x => $"{x.Name} - {x.Currency.Code}"));
        }
    }
}
