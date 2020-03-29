namespace AccounterApplication.Web.Controllers
{
    using System;

    using AutoMapper;
    using Newtonsoft.Json;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;

    using Web.ViewModels;
    using Services.Mapping;
    using Common.Enumerations;

    public class BaseController : Controller
    {
        private IMapper mapper;

        protected IMapper Mapper
        {
            get
            {
                if (this.mapper == null)
                {
                    this.mapper = AutoMapperConfig.MapperInstance;
                }

                return this.mapper;
            }
        }

        protected void CreateCultureCookie(string culture)
            => Response.Cookies.Append(
               CookieRequestCultureProvider.DefaultCookieName,
               CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
               new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

        protected void AddAlertMessageToTempData(AlertMessageTypes alertMessageType, string message)
        {
            switch (alertMessageType)
            {
                case AlertMessageTypes.Success:
                    TempData["UserAlertMessage"] = JsonConvert.SerializeObject(new MessageAlertViewModel("alert-success", "fa-check", "Success!", message));
                    break;
                case AlertMessageTypes.Info:
                    break;
                case AlertMessageTypes.Error:
                    TempData["UserAlertMessage"] = JsonConvert.SerializeObject(new MessageAlertViewModel("alert-danger", "fa-times ", "Error!", message));
                    break;
                default:
                    break;
            }
        }
    }
}
