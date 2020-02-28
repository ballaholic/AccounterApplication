﻿namespace AccounterApplication.Web.ViewModels.Expenses
{
    using System;
    using AutoMapper;
    using Data.Models;
    using Services.Mapping;

    public class ExpenseViewModel : IMapFrom<Expense>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal ExpenseAmount { get; set; }

        public string UserName { get; set; }

        public DateTime CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Expense, ExpenseViewModel>().ForMember(
                m => m.UserName,
                opt => opt.MapFrom(x => x.User.UserName));
        }
    }
}
