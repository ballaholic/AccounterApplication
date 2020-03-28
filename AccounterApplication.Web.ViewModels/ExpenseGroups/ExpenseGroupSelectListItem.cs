namespace AccounterApplication.Web.ViewModels.ExpenseGroups
{
    using Microsoft.AspNetCore.Mvc.Rendering;

    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class ExpenseGroupSelectListItem : SelectListItem, IMapFrom<ExpenseGroup>, IHaveCustomMappings
    {
        public void CreateMappings(IProfileExpression configuration)
            => configuration.CreateMap<ExpenseGroup, ExpenseGroupSelectListItem>().ForMember(
                m => m.Value,
                opt => opt.MapFrom(x => x.Id))
            .ForMember(
                m => m.Text,
                opt => opt.MapFrom(x => x.NameBG));
    }
}
