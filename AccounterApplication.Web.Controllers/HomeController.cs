namespace AccounterApplication.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Localization;

    using ViewModels;

    public class HomeController : BaseController
    {
        private readonly IStringLocalizer<HomeController> localizer;

        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            this.localizer = localizer;
        }

        public IActionResult Index() 
        {
            return View();
        }    

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            => View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });

        public IActionResult SetCulture(string culture)
        {
            this.CreateCultureCookie(culture);
            return this.Json(culture);
        }
    }
}
