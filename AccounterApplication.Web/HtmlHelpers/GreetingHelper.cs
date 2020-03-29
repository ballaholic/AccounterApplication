namespace AccounterApplication.Web.HtmlHelpers
{
    public static class GreetingHelper
    {
        public static string GetGreet(int currentHour)
        {
            if (currentHour < 5)
            {
                return "Hello";
            }
            else if (currentHour >= 5 && currentHour < 8)
            {
                return "Good Morning";
            }
            else if (currentHour >= 8 && currentHour < 12)
            {
                return "Hello";
            }
            else if (currentHour >= 12 && currentHour < 18)
            {
                return "Good Afternoon";
            }
            else if (currentHour >= 18 && currentHour < 24)
            {
                return "Good Evening";
            }
            else
            {
                return "Hello";
            }
        }
    }
}
