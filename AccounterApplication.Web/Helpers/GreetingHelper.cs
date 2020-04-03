namespace AccounterApplication.Web.Helpers
{
    using System;

    using Resources = Common.LocalizationResources.Shared.Helpers.GreetingHelperResources;

    public static class GreetingHelper
    {
        public static string GreetingMessage(string userName)
        {
            string greet = GetGreet();

            return $"{greet}, {userName}!";
        }

        private static string GetGreet()
        {
            var currentHour = DateTime.Now.Hour;

            if (currentHour < 5)
            {
                return Resources.Hello;
            }
            else if (currentHour >= 5 && currentHour < 8)
            {
                return Resources.GoodMorning;
            }
            else if (currentHour >= 8 && currentHour < 12)
            {
                return Resources.Hello;
            }
            else if (currentHour >= 12 && currentHour < 18)
            {
                return Resources.GoodAfternoon;
            }
            else if (currentHour >= 18 && currentHour < 24)
            {
                return Resources.GoodEvening;
            }
            else
            {
                return Resources.Hello;
            }
        }
    }
}
