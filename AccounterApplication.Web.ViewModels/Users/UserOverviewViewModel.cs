namespace AccounterApplication.Web.ViewModels.Users
{
    using System;
    using System.Linq;
    using Data.Models;
    using Common.GlobalConstants;

    public class UserOverviewViewModel
    {
        public string UserName { get; set; }
        public decimal? EarningsMonthly { get; set; }
        public decimal? EarningsAnnual { get; set; }
        public double TasksCompletion { get; set; }

        public static UserOverviewViewModel FromApplicationUser(ApplicationUser user)
        {
            return new UserOverviewViewModel()
            {
                UserName = user.UserName,
                EarningsMonthly = user.MonthlyIncomes.FirstOrDefault(y => y.CreatedOn.Month.Equals(DateTime.UtcNow.Month)) == null
                    ? UserConstants.MinEarning 
                    : user.MonthlyIncomes.FirstOrDefault(y => y.CreatedOn.Month.Equals(DateTime.UtcNow.Month)).Amount,
                EarningsAnnual = CalculateCurrentAnnualEarnings(user),
                TasksCompletion = 50
            };
        }

        private static decimal? CalculateCurrentAnnualEarnings(ApplicationUser user)
        {
            var currentYear = DateTime.UtcNow.Year;

            decimal? amount = user.MonthlyIncomes
                .Where(x => x.CreatedOn.Year.Equals(currentYear))
                .Select(x => x.Amount)
                .Sum();

            return amount;
        }


    }
}
