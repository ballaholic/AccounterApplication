﻿namespace AccounterApplication.Web.Controllers
{
    using System;

    using AutoMapper;
    using Newtonsoft.Json;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Localization;

    using Web.ViewModels;
    using Infrastructure;
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

        protected void AddAlertMessageToTempData(AlertMessageTypes alertMessageType, string title, string message)
        {
            switch (alertMessageType)
            {
                case AlertMessageTypes.Success:
                    TempData["UserAlertMessage"] = JsonConvert.SerializeObject(new MessageAlertViewModel("alert-success", "fa-check", title, message));
                    break;
                case AlertMessageTypes.Info:
                    break;
                case AlertMessageTypes.Error:
                    TempData["UserAlertMessage"] = JsonConvert.SerializeObject(new MessageAlertViewModel("alert-danger", "fa-times ", title, message));
                    break;
                default:
                    break;
            }
        }
    }
}
