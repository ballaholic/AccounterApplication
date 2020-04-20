namespace AccounterApplication.Web.Areas.Administration.Controllers
{
    using System;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;

    using Infrastructure;
    using Common.Enumerations;

    [Area("Administration")]
    public class BaseController : Controller
    {
        protected Languages GetCurrentLanguage()
        {
            string langCookie = this.Request.Cookies[CookieRequestCultureProvider.DefaultCookieName];

            if (langCookie != null && langCookie.Contains("bg-BG"))
            {
                return Languages.Bulgarian;
            }
            else
            {
                return Languages.English;
            }
        }

        protected T GetUserId<T>() => this.User.GetLoggedInUserId<T>();

        protected void CreateCultureCookie(string culture)
            => Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
    }
}
