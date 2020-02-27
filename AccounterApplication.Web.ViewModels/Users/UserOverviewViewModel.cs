namespace AccounterApplication.Web.ViewModels.Users
{
    using System;

    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class UserOverviewViewModel : IMapFrom<ApplicationUser>, IHaveCustomMappings
    {
        public decimal EarningsMonthly { get; set; }
        public decimal EarningsAnnual { get; set; }
        public double TasksCompletion { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            throw new NotImplementedException();
        }
    }
}
